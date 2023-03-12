﻿using Dapper;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.OleDb;
using System.Runtime.Versioning;

namespace Schotejil.Clubkampioen.Data.TeamManager;

[SupportedOSPlatform("windows")]
public class TeamManagerDatabase : ITeamManagerDatabase
{
    private static string ConnectionString =>
        @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\ProgramData\Team Manager\Team.mdb";

    private readonly ILogger<TeamManagerDatabase> logger;

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

    public bool TestConnection()
    {
        using var connection = new OleDbConnection(ConnectionString);
        try
        {
            connection.Open();
            this.logger.LogInformation("Sucessfully connected to Team Manager database");
            return true;
        }
        catch
        {
            return false;
        }
    }

    public IEnumerable<Meet> GetMeets()
    {
        using var connection = new OleDbConnection(ConnectionString);
        return connection.Query<Meet>("select * from MEETS order by MAXDATE desc");
    }

    public IEnumerable<Meet> GetMeetsWithRelays()
    {
        using var connection = new OleDbConnection(ConnectionString);
        return connection.Query<Meet>("select distinct m.* from (MEETS m inner join EVENTS e on e.MEETSID = m.MEETSID) inner join SWIMSTYLE s on e.STYLESID = s.SWIMSTYLEID where s.RELAYCOUNT > 1 order by m.MAXDATE desc");
    }

    public IEnumerable<Event> GetEvents(int meetId)
    {
        using var connection = new OleDbConnection(ConnectionString);
        return connection.Query<Event>("select * from EVENTS where MEETSID=@Id", new { Id = meetId });
    }

    public IEnumerable<Event> GetRelays(int meetId)
    {
        using var connection = new OleDbConnection(ConnectionString);
        return connection.Query<Event, SwimStyle, Event>(
            "select * from EVENTS e INNER JOIN SWIMSTYLE s on e.STYLESID = s.SWIMSTYLEID where e.MEETSID = @Id and s.RELAYCOUNT > 1",
            (ev, swimStyle) => ev with { SwimStyle = swimStyle },
            new { Id = meetId },
            splitOn: "SWIMSTYLEID");
    }

    public IEnumerable<Member> GetMembers()
    {
        using var connection = new OleDbConnection(ConnectionString);
        return connection.Query<Member>("select * from MEMBERS");
    }

    public IEnumerable<Member> GetMembers(int meetId)
    {
        using var connection = new OleDbConnection(ConnectionString);
        return connection.Query<Member>(
            "select distinct m.* from RESULTS r INNER JOIN MEMBERS m on r.MEMBERSID = m.MEMBERSID where r.MEETSID = @Id",
            new { Id = meetId });
    }

    public IEnumerable<Group> GetGroups()
    {
        using var connection = new OleDbConnection(ConnectionString);
        return connection.Query<Group>("select * from GROUPS");
    }

    public IEnumerable<(Member Member, TimeSpan EntryTime)> GetFastestMembers(SwimStyle swimStyle, Gender gender, int minAge, int maxAge, DateTime ageDate, IEnumerable<Member> availableMembers)
    {
        DateTime minDate = ageDate.AddYears(-maxAge);
        DateTime maxDate = ageDate.AddYears(-minAge);
        using var connection = new OleDbConnection(ConnectionString);
        int styleId = connection.QueryFirst<int>("select SWIMSTYLEID from SWIMSTYLE where DISTANCE = @Distance and STROKE = @Stroke", new { swimStyle.Distance, swimStyle.Stroke });

        return connection.Query<Member, int, (Member Member, TimeSpan EntryTime)>(
            "select m.MEMBERSID, m.FIRSTNAME, m.LASTNAME, m.BIRTHDATE, m.GROUPS, min(r.TOTALTIME) as TOTALTIME from MEMBERS m inner join RESULTS r on m.MEMBERSID = r.MEMBERSID where r.STYLESID = @StyleId and m.GENDER = @Gender and m.BIRTHDATE between @MinDate and @MaxDate and m.MEMBERSID in @AvailableMembers and r.TOTALTIME > 0 group by m.MEMBERSID, m.FIRSTNAME, m.LASTNAME, m.BIRTHDATE, m.GROUPS order by min(r.TOTALTIME)",
            (member, entryTime) => (member, TimeSpan.FromMilliseconds(entryTime)),
            new { StyleId = styleId, Gender = (int)gender, MinDate = minDate, MaxDate = maxDate, AvailableMembers = availableMembers.Select(m => m.Id) },
            splitOn: "TOTALTIME")
            .Take(swimStyle.RelayCount);
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
