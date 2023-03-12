namespace Schotejil.Clubkampioen.Data.TeamManager;

public interface ITeamManagerDatabase
{
    Task<IEnumerable<Event>> GetEvents(int meetId);
    Task<IEnumerable<(Member Member, TimeSpan EntryTime)>> GetFastestMembers(int distance, Stroke stroke, Gender gender, int minAge, int maxAge, DateTime ageDate, IEnumerable<Member> availableMembers);
    Task<IEnumerable<Group>> GetGroups();
    Task<IEnumerable<Meet>> GetMeets();
    Task<IEnumerable<Meet>> GetMeetsWithRelays();
    Task<IEnumerable<Member>> GetMembers();
    Task<IEnumerable<Member>> GetMembers(int meetId);
    Task<IEnumerable<Event>> GetRelays(int meetId);
}