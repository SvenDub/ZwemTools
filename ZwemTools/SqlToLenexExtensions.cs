// <copyright file="SqlToLenexExtensions.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

using ZwemTools.Data.Lenex.Xml;
using ZwemTools.Data.Sql;
using AgeDateType = ZwemTools.Data.Lenex.Xml.AgeDateType;
using Course = ZwemTools.Data.Lenex.Xml.Course;
using Entry = ZwemTools.Data.Sql.Entry;
using Gender = ZwemTools.Data.Lenex.Xml.Gender;
using ResultStatus = ZwemTools.Data.Lenex.Xml.ResultStatus;
using Stroke = ZwemTools.Data.Lenex.Xml.Stroke;

namespace ZwemTools;

public static class SqlToLenexExtensions
{
    public static MeetElement ToLenex(this Meet sql) => new()
    {
        Name = sql.Name,
        Course = sql.Course?.ToLenex(),
        City = sql.City,
        Organizer = sql.Organizer,
        OrganizerUrl = sql.OrganizerUrl,
        LiveTiming = sql.LiveTiming,
        Nation = sql.Nation,
        AgeDate = sql.AgeDate?.ToLenex(),
        Clubs = sql.Clubs.Select(club => club.ToLenex()).ToCollection(),
        Sessions = sql.Sessions.Select(session => session.ToLenex()).ToCollection(),
    };

    public static Course ToLenex(this Data.Sql.Course sql) => sql switch
    {
        Data.Sql.Course.Scm => Course.Scm,
        Data.Sql.Course.Lcm => Course.Lcm,
        Data.Sql.Course.Scy => Course.Scy,
        Data.Sql.Course.Scm16 => Course.Scm16,
        Data.Sql.Course.Scm20 => Course.Scm20,
        Data.Sql.Course.Scm33 => Course.Scm33,
        Data.Sql.Course.Scy20 => Course.Scy20,
        Data.Sql.Course.Scy27 => Course.Scy27,
        Data.Sql.Course.Scy36 => Course.Scy36,
        Data.Sql.Course.Open => Course.Open,
        _ => throw new ArgumentOutOfRangeException(nameof(sql), sql, "Unknown value"),
    };

    public static AgeDateElement ToLenex(this AgeDate sql) => new()
    {
        Type = sql.Type.ToLenex(),
        Value = sql.Value.ToDateTime(TimeOnly.MinValue),
    };

    public static AgeDateType ToLenex(this Data.Sql.AgeDateType sql) => sql switch
    {
        Data.Sql.AgeDateType.Year => AgeDateType.Year,
        Data.Sql.AgeDateType.Date => AgeDateType.Date,
        Data.Sql.AgeDateType.Por => AgeDateType.Por,
        Data.Sql.AgeDateType.CanFnq => AgeDateType.CanFnq,
        Data.Sql.AgeDateType.Lux => AgeDateType.Lux,
        _ => throw new ArgumentOutOfRangeException(nameof(sql), sql, "Unknown value"),
    };

    public static ClubElement ToLenex(this Club sql) => new()
    {
        Name = sql.Name,
        ClubId = sql.LenexId,
        Athletes = sql.Athletes.Select(athlete => athlete.ToLenex()).ToCollection(),
    };

    public static SessionElement ToLenex(this Session sql) => new()
    {
        Name = sql.Name,
        Date = sql.Date.ToDateTime(TimeOnly.MinValue),
        Number = sql.Number,
        StartTime = sql.StartTime,
        EndTime = sql.EndTime,
        OfficialMeeting = sql.OfficialMeeting,
        WarmupStart = sql.WarmupStart,
        WarmupEnd = sql.WarmupEnd,
        Events = sql.Events.Select(ev => ev.ToLenex()).ToCollection(),
    };

    public static AthleteElement ToLenex(this Athlete sql) => new()
    {
        FirstName = sql.FirstName,
        NamePrefix = sql.NamePrefix,
        LastName = sql.LastName,
        Birthdate = sql.Birthdate.ToDateTime(TimeOnly.MinValue),
        AthleteId = sql.LenexId,
        License = sql.License,
        Gender = sql.Gender.ToLenex(),
        Entries = sql.Entries.Select(entry => entry.ToLenex()).ToCollection(),
        Results = sql.Results.Select(result => result.ToLenex()).ToCollection(),
    };

    public static Gender ToLenex(this Data.Sql.Gender sql) => sql switch
    {
        Data.Sql.Gender.All => Gender.All,
        Data.Sql.Gender.Female => Gender.Female,
        Data.Sql.Gender.Male => Gender.Male,
        Data.Sql.Gender.Mixed => Gender.Mixed,
        _ => throw new ArgumentOutOfRangeException(nameof(sql), sql, "Unknown value"),
    };

    public static EventElement ToLenex(this Event sql) => new()
    {
        EventId = sql.LenexId,
        Number = sql.Number,
        Time = sql.Time,
        Gender = sql.Gender?.ToLenex(),
        SwimStyle = sql.SwimStyle.ToLenex(),
        AgeGroups = sql.AgeGroups.Select(group => group.ToLenex()).ToCollection(),
        Heats = sql.Heats.OrderBy(heat => heat.Order).Select(heat => heat.ToLenex()).ToCollection(),
    };

    public static SwimStyleElement ToLenex(this SwimStyle sql) => new()
    {
        Distance = sql.Distance,
        RelayCount = sql.RelayCount,
        Stroke = sql.Stroke.ToLenex(),
    };

    public static Stroke ToLenex(this Data.Sql.Stroke sql) => sql switch
    {
        Data.Sql.Stroke.Apnea => Stroke.Apnea,
        Data.Sql.Stroke.Backstroke => Stroke.Backstroke,
        Data.Sql.Stroke.BiFins => Stroke.BiFins,
        Data.Sql.Stroke.Breaststroke => Stroke.Breaststroke,
        Data.Sql.Stroke.Fly => Stroke.Fly,
        Data.Sql.Stroke.Freestyle => Stroke.Freestyle,
        Data.Sql.Stroke.Immersion => Stroke.Immersion,
        Data.Sql.Stroke.IndividualMedleyRelay => Stroke.IndividualMedleyRelay,
        Data.Sql.Stroke.Medley => Stroke.Medley,
        Data.Sql.Stroke.Surface => Stroke.Surface,
        Data.Sql.Stroke.Unknown => Stroke.Unknown,
        _ => throw new ArgumentOutOfRangeException(nameof(sql), sql, "Unknown value"),
    };

    public static AgeGroupElement ToLenex(this AgeGroup sql) => new()
    {
        Gender = sql.Gender?.ToLenex(),
        AgeGroupId = sql.LenexId,
        MinAge = sql.MinAge,
        MaxAge = sql.MaxAge,
        Rankings = sql.Rankings.Select(ranking => ranking.ToLenex()).ToCollection(),
    };

    public static RankingElement ToLenex(this Ranking sql) => new()
    {
        Order = sql.Order,
        Place = sql.Place,
        ResultId = sql.LenexResultId,
    };

    public static HeatElement ToLenex(this Heat sql) => new()
    {
        Number = sql.Number,
        HeatId = sql.LenexId,
        Order = sql.Order,
    };

    public static EntryElement ToLenex(this Entry sql) => new()
    {
        EventId = sql.LenexEventId,
        HeatId = sql.Heat?.LenexId,
        Lane = sql.Lane,
        EntryTime = sql.EntryTime,
        Course = sql.Course?.ToLenex(),
    };

    public static ResultElement ToLenex(this Result sql) => new()
    {
        EventId = sql.LenexEventId,
        HeatId = null, // TODO
        ResultId = sql.LenexId,
        Lane = sql.Lane,
        SwimTime = sql.SwimTime,
        Status = sql.Status?.ToLenex(),
    };

    public static ResultStatus ToLenex(this Data.Sql.ResultStatus sql) => sql switch
    {
        Data.Sql.ResultStatus.Exhibition => ResultStatus.Exhibition,
        Data.Sql.ResultStatus.Disqualified => ResultStatus.Disqualified,
        Data.Sql.ResultStatus.DidNotStart => ResultStatus.DidNotStart,
        Data.Sql.ResultStatus.DidNotFinish => ResultStatus.DidNotFinish,
        Data.Sql.ResultStatus.Sick => ResultStatus.Sick,
        Data.Sql.ResultStatus.Withdrawn => ResultStatus.Withdrawn,
        _ => throw new ArgumentOutOfRangeException(nameof(sql), sql, "Unknown value"),
    };
}
