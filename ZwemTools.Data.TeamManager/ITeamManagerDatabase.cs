// <copyright file="ITeamManagerDatabase.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

namespace ZwemTools.Data.TeamManager;

public interface ITeamManagerDatabase : IAsyncDisposable
{
    Task<bool> TestConnection();

    Task<IEnumerable<Event>> GetEvents(int meetId);

    Task<IEnumerable<(Member Member, TimeSpan EntryTime)>> GetFastestMembers(
        SwimStyle swimStyle,
        Gender? gender,
        int minAge,
        int maxAge,
        DateTime ageDate,
        IEnumerable<Member> availableMembers);

    Task<IEnumerable<Group>> GetGroups();

    Task<IEnumerable<Meet>> GetMeets();

    Task<IEnumerable<Meet>> GetMeetsWithRelays();

    Task<IEnumerable<Member>> GetMembers();

    Task<IEnumerable<Member>> GetMembers(int meetId);

    Task<IEnumerable<Event>> GetRelays(int meetId);

    /// <summary>
    /// Gets the swim style matching the given parameters.
    /// </summary>
    /// <param name="distance">The distance.</param>
    /// <param name="stroke">The stroke.</param>
    /// <param name="relayCount">The relay count.</param>
    /// <returns>The swim style matching the parameters, or null if not found.</returns>
    Task<SwimStyle?> GetSwimStyle(int distance, Stroke stroke, int relayCount = 1);
}
