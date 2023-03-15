// <copyright file="TeamManagerDatabase.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.OleDb;
using System.Runtime.Versioning;
using Dapper;
using Microsoft.Extensions.Logging;

namespace ZwemTools.Data.TeamManager;

[SupportedOSPlatform("windows")]
public class TeamManagerDatabase : ITeamManagerDatabase
{
    private readonly ILogger<TeamManagerDatabase> logger;

    private static string ConnectionString =>
        @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\ProgramData\Team Manager\Team.mdb";

    public TeamManagerDatabase(ILogger<TeamManagerDatabase> logger)
    {
        this.logger = logger;
        MapAttributes<Meet>();
        MapAttributes<Event>();
        MapAttributes<SwimStyle>();
        MapAttributes<Member>();
        MapAttributes<Group>();
        MapAttributes<RelayPosition>();
    }

    public async Task<bool> TestConnection()
    {
        await using OleDbConnection connection = new(ConnectionString);
        try
        {
            await connection.OpenAsync();
            this.logger.LogInformation("Successfully connected to Team Manager database");
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<IEnumerable<Meet>> GetMeets()
    {
        await using OleDbConnection connection = new(ConnectionString);
        return await connection.QueryAsync<Meet>("select * from MEETS order by MAXDATE desc");
    }

    public async Task<IEnumerable<Meet>> GetMeetsWithRelays()
    {
        await using OleDbConnection connection = new(ConnectionString);
        return await connection.QueryAsync<Meet>(
            "select distinct m.* from (MEETS m inner join EVENTS e on e.MEETSID = m.MEETSID) inner join SWIMSTYLE s on e.STYLESID = s.SWIMSTYLEID where s.RELAYCOUNT > 1 order by m.MAXDATE desc");
    }

    public async Task<IEnumerable<Event>> GetEvents(int meetId)
    {
        await using OleDbConnection connection = new(ConnectionString);
        return await connection.QueryAsync<Event>("select * from EVENTS where MEETSID=@Id", new { Id = meetId });
    }

    public async Task<IEnumerable<Event>> GetRelays(int meetId)
    {
        await using OleDbConnection connection = new(ConnectionString);
        return await connection.QueryAsync<Event, SwimStyle, Event>(
            "select * from EVENTS e INNER JOIN SWIMSTYLE s on e.STYLESID = s.SWIMSTYLEID where e.MEETSID = @Id and s.RELAYCOUNT > 1",
            (ev, swimStyle) => ev with { SwimStyle = swimStyle },
            new { Id = meetId },
            splitOn: "SWIMSTYLEID");
    }

    public async Task<IEnumerable<Member>> GetMembers()
    {
        await using OleDbConnection connection = new(ConnectionString);
        return await connection.QueryAsync<Member>("select * from MEMBERS");
    }

    public async Task<IEnumerable<Member>> GetMembers(int meetId)
    {
        await using OleDbConnection connection = new(ConnectionString);
        return await connection.QueryAsync<Member>(
            "select distinct m.* from RESULTS r INNER JOIN MEMBERS m on r.MEMBERSID = m.MEMBERSID where r.MEETSID = @Id",
            new { Id = meetId });
    }

    public async Task<IEnumerable<Group>> GetGroups()
    {
        await using OleDbConnection connection = new(ConnectionString);
        return await connection.QueryAsync<Group>("select * from GROUPS");
    }

    public async Task<IEnumerable<(Member Member, TimeSpan EntryTime)>> GetFastestMembers(
        int distance,
        Stroke stroke,
        Gender gender,
        int minAge,
        int maxAge,
        DateTime ageDate,
        IEnumerable<Member> availableMembers)
    {
        DateTime minDate = ageDate.AddYears(-maxAge);
        DateTime maxDate = ageDate.AddYears(-minAge);
        await using OleDbConnection connection = new(ConnectionString);
        int styleId = await connection.QueryFirstAsync<int>(
            "select SWIMSTYLEID from SWIMSTYLE where DISTANCE = @Distance and STROKE = @Stroke",
            new { Distance = distance, Stroke = stroke });

        return await connection.QueryAsync<Member, int, (Member Member, TimeSpan EntryTime)>(
            "select m.MEMBERSID, m.FIRSTNAME, m.LASTNAME, m.BIRTHDATE, m.GROUPS, min(r.TOTALTIME) as TOTALTIME from MEMBERS m inner join RESULTS r on m.MEMBERSID = r.MEMBERSID where r.STYLESID = @StyleId and m.GENDER = @Gender and m.BIRTHDATE between @MinDate and @MaxDate and m.MEMBERSID in @AvailableMembers and r.TOTALTIME > 0 group by m.MEMBERSID, m.FIRSTNAME, m.LASTNAME, m.BIRTHDATE, m.GROUPS order by min(r.TOTALTIME)",
            (member, entryTime) => (member, TimeSpan.FromMilliseconds(entryTime)),
            new { StyleId = styleId, Gender = (int)gender, MinDate = minDate, MaxDate = maxDate, AvailableMembers = availableMembers.Select(m => m.Id) },
            splitOn: "TOTALTIME");
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
}
