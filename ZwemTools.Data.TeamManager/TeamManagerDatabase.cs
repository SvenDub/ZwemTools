// <copyright file="TeamManagerDatabase.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.OleDb;
using System.Runtime.Versioning;
using Dapper;
using Microsoft.Extensions.Logging;
using ZwemTools.Abstractions;

namespace ZwemTools.Data.TeamManager;

/// <inheritdoc />
[SupportedOSPlatform("windows")]
public sealed class TeamManagerDatabase : ITeamManagerDatabase
{
    private readonly IPreferenceService preferenceService;
    private readonly ILogger<TeamManagerDatabase> logger;
    private readonly object @lock = new();

    private OleDbConnection? sharedConnection;

    /// <summary>
    /// Initializes a new instance of the <see cref="TeamManagerDatabase"/> class.
    /// </summary>
    /// <param name="preferenceService">The preferences.</param>
    /// <param name="logger">The logger.</param>
    public TeamManagerDatabase(IPreferenceService preferenceService, ILogger<TeamManagerDatabase> logger)
    {
        this.preferenceService = preferenceService;
        this.logger = logger;
        MapAttributes<Meet>();
        MapAttributes<Event>();
        MapAttributes<SwimStyle>();
        MapAttributes<Member>();
        MapAttributes<Group>();
        MapAttributes<RelayPosition>();
    }

    private string ConnectionString => $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={this.preferenceService.TeamManagerFile}";

    /// <inheritdoc/>
    public async Task<bool> TestConnection()
    {
        if (!File.Exists(this.preferenceService.TeamManagerFile))
        {
            return false;
        }

        try
        {
            OleDbConnection connection = this.GetConnection();
            if (connection.State == ConnectionState.Closed)
            {
                await connection.OpenAsync();
                this.logger.LogDebug("Successfully connected to Team Manager database");
            }

            if (connection.State == ConnectionState.Open)
            {
                return true;
            }
        }
        catch (Exception e)
        {
            this.logger.LogError(e, "Could not connect to Team Manager database");
        }

        return false;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Meet>> GetMeets()
    {
        OleDbConnection connection = this.GetConnection();
        return await connection.QueryAsync<Meet>("select * from MEETS order by MAXDATE desc");
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Meet>> GetMeetsWithRelays()
    {
        OleDbConnection connection = this.GetConnection();
        return await connection.QueryAsync<Meet>(
            "select distinct m.* from (MEETS m inner join EVENTS e on e.MEETSID = m.MEETSID) inner join SWIMSTYLE s on e.STYLESID = s.SWIMSTYLEID where s.RELAYCOUNT > 1 order by m.MAXDATE desc");
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Event>> GetEvents(int meetId)
    {
        OleDbConnection connection = this.GetConnection();
        return await connection.QueryAsync<Event>("select * from EVENTS where MEETSID=@Id", new { Id = meetId });
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Event>> GetRelays(int meetId)
    {
        OleDbConnection connection = this.GetConnection();
        return await connection.QueryAsync<Event, SwimStyle, Event>(
            "select * from EVENTS e INNER JOIN SWIMSTYLE s on e.STYLESID = s.SWIMSTYLEID where e.MEETSID = @Id and s.RELAYCOUNT > 1",
            (ev, swimStyle) => ev with { SwimStyle = swimStyle },
            new { Id = meetId },
            splitOn: "SWIMSTYLEID");
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Member>> GetMembers()
    {
        OleDbConnection connection = this.GetConnection();
        return await connection.QueryAsync<Member>("select * from MEMBERS");
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Member>> GetMembers(int meetId)
    {
        OleDbConnection connection = this.GetConnection();
        return await connection.QueryAsync<Member>(
            "select distinct m.* from RESULTS r INNER JOIN MEMBERS m on r.MEMBERSID = m.MEMBERSID where r.MEETSID = @Id",
            new { Id = meetId });
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Group>> GetGroups()
    {
        OleDbConnection connection = this.GetConnection();
        return await connection.QueryAsync<Group>("select * from GROUPS");
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<(Member Member, TimeSpan EntryTime)>> GetFastestMembers(
        SwimStyle swimStyle,
        Gender? gender,
        IEnumerable<Member> availableMembers,
        DateTime minimumEntryDate)
    {
        OleDbConnection connection = this.GetConnection();
        Gender[] genders = gender is { } g ? new[] { g } : new[] { Gender.Male, Gender.Female };

        return await connection.QueryAsync<Member, int, (Member Member, TimeSpan EntryTime)>(
            "select m.MEMBERSID, m.FIRSTNAME, m.LASTNAME, m.BIRTHDATE, m.GROUPS, min(r.TOTALTIME) as TOTALTIME from MEMBERS m inner join RESULTS r on m.MEMBERSID = r.MEMBERSID where r.STYLESID = @StyleId and m.GENDER in @Gender and m.MEMBERSID in @AvailableMembers and r.TOTALTIME > 0 and r.EVENTDATE >= @MinEventDate group by m.MEMBERSID, m.FIRSTNAME, m.LASTNAME, m.BIRTHDATE, m.GROUPS order by min(r.TOTALTIME)",
            (member, entryTime) => (member, TimeSpan.FromMilliseconds(entryTime)),
            new { StyleId = swimStyle.Id, Gender = genders, AvailableMembers = availableMembers.Select(m => m.Id), MinEventDate = minimumEntryDate },
            splitOn: "TOTALTIME");
    }

    /// <inheritdoc/>
    public async Task<SwimStyle?> GetSwimStyle(int distance, Stroke stroke, int relayCount = 1)
    {
        OleDbConnection connection = this.GetConnection();
        return await connection.QueryFirstOrDefaultAsync<SwimStyle>(
            "select * from SWIMSTYLE where DISTANCE = @Distance and STROKE = @Stroke and RELAYCOUNT = @RelayCount",
            new { Distance = distance, Stroke = stroke, RelayCount = relayCount });
    }

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        if (this.sharedConnection is not null)
        {
            this.logger.LogDebug("Closing Team Manager connection");
            await this.sharedConnection.DisposeAsync();
        }
    }

    private static void MapAttributes<T>()
    {
        SqlMapper.SetTypeMap(
            typeof(T),
            new CustomPropertyTypeMap(
                typeof(T),
                (type, columnName) =>
                    type.GetProperties().FirstOrDefault(prop =>
                        prop.GetCustomAttributes(false)
                            .OfType<ColumnAttribute>()
                            .Any(attr => attr.Name == columnName))));
    }

    private OleDbConnection GetConnection()
    {
        if (this.sharedConnection is not null)
        {
            return this.sharedConnection;
        }

        lock (this.@lock)
        {
            if (this.sharedConnection is null)
            {
                this.logger.LogDebug("Connecting to Team Manager database");
                this.sharedConnection = new OleDbConnection(this.ConnectionString);
            }
        }

        return this.sharedConnection;
    }
}
