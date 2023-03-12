namespace Schotejil.Clubkampioen.Data.TeamManager;

public interface ITeamManagerDatabase
{
    IEnumerable<Event> GetEvents(int meetId);
    IEnumerable<(Member Member, TimeSpan EntryTime)> GetFastestMembers(int distance, Stroke stroke, Gender gender, int minAge, int maxAge, DateTime ageDate, IEnumerable<Member> availableMembers);
    IEnumerable<Group> GetGroups();
    IEnumerable<Meet> GetMeets();
    IEnumerable<Meet> GetMeetsWithRelays();
    IEnumerable<Member> GetMembers();
    IEnumerable<Member> GetMembers(int meetId);
    IEnumerable<Event> GetRelays(int meetId);
}