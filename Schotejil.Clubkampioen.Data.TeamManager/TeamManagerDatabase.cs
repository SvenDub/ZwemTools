using Dapper;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.OleDb;
using System.Runtime.Versioning;

namespace Schotejil.Clubkampioen.Data.TeamManager;

[SupportedOSPlatform("windows")]
public class TeamManagerDatabase
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
    }

    public bool TestConnection()
    {
        using var connection = new OleDbConnection(ConnectionString);
        try
        {
            connection.Open();
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
