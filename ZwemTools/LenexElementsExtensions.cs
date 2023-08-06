﻿// <copyright file="LenexElementsExtensions.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

using ZwemTools.Data.Lenex.Xml;
using ZwemTools.Data.Sql;
using Entry = ZwemTools.Data.Sql.Entry;

namespace ZwemTools;

public static class LenexElementsExtensions
{
    public static Meet ToSql(this MeetElement lenex)
    {
        Meet sql = new()
        {
            AgeDate = lenex.AgeDate?.ToSql(),
            City = lenex.City,
            Name = lenex.Name,
            Course = lenex.Course?.ToSql(),
            Nation = lenex.Nation,
            Organizer = lenex.Organizer,
            OrganizerUrl = lenex.OrganizerUrl,
            LiveTiming = lenex.LiveTiming,
            Sessions = lenex.Sessions.Select(x => x.ToSql()).ToList(),
            Clubs = lenex.Clubs.Select(x => x.ToSql()).ToList(),
        };

        foreach (Session session in sql.Sessions)
        {
            foreach (Event @event in session.Events)
            {
                foreach (AgeGroup ageGroup in @event.AgeGroups)
                {
                    foreach (Ranking ranking in ageGroup.Rankings)
                    {
                        ranking.Result = sql.Clubs.SelectMany(x => x.Athletes).SelectMany(x => x.Results).Single(x => x.Id == ranking.ResultId);
                        ranking.ResultId = default;
                    }
                }
            }
        }

        foreach (Club club in sql.Clubs)
        {
            foreach (Athlete athlete in club.Athletes)
            {
                foreach (Result result in athlete.Results)
                {
                    result.Event = sql.Sessions.SelectMany(x => x.Events).Single(x => x.Id == result.EventId);
                    result.EventId = default;
                    result.Id = default;
                }

                foreach (Entry entry in athlete.Entries)
                {
                    entry.Event = sql.Sessions.SelectMany(x => x.Events).Single(x => x.Id == entry.EventId);
                    entry.EventId = default;
                }
            }
        }

        foreach (Session session in sql.Sessions)
        {
            foreach (Event @event in session.Events)
            {
                @event.Id = default;
            }
        }

        return sql;
    }

    public static Club ToSql(this ClubElement lenex) => new()
    {
        Name = lenex.Name,
        Athletes = lenex.Athletes.Select(x => x.ToSql()).ToList(),
    };

    private static AgeDate ToSql(this AgeDateElement lenex) => new()
    {
        Type = lenex.Type.ToSql(),
        Value = DateOnly.FromDateTime(lenex.Value),
    };

    private static Data.Sql.AgeDateType ToSql(this Data.Lenex.Xml.AgeDateType lenex) => lenex switch
    {
        Data.Lenex.Xml.AgeDateType.Year => Data.Sql.AgeDateType.Year,
        Data.Lenex.Xml.AgeDateType.Date => Data.Sql.AgeDateType.Date,
        Data.Lenex.Xml.AgeDateType.Por => Data.Sql.AgeDateType.Por,
        Data.Lenex.Xml.AgeDateType.CanFnq => Data.Sql.AgeDateType.CanFnq,
        Data.Lenex.Xml.AgeDateType.Lux => Data.Sql.AgeDateType.Lux,
        _ => throw new ArgumentOutOfRangeException(nameof(lenex), lenex, "Unknown value"),
    };

    private static Data.Sql.Course ToSql(this Data.Lenex.Xml.Course lenex) => lenex switch
    {
        Data.Lenex.Xml.Course.Scm => Data.Sql.Course.Scm,
        Data.Lenex.Xml.Course.Lcm => Data.Sql.Course.Lcm,
        Data.Lenex.Xml.Course.Scy => Data.Sql.Course.Scy,
        Data.Lenex.Xml.Course.Scm16 => Data.Sql.Course.Scm16,
        Data.Lenex.Xml.Course.Scm20 => Data.Sql.Course.Scm20,
        Data.Lenex.Xml.Course.Scm33 => Data.Sql.Course.Scm33,
        Data.Lenex.Xml.Course.Scy20 => Data.Sql.Course.Scy20,
        Data.Lenex.Xml.Course.Scy27 => Data.Sql.Course.Scy27,
        Data.Lenex.Xml.Course.Scy36 => Data.Sql.Course.Scy36,
        Data.Lenex.Xml.Course.Open => Data.Sql.Course.Open,
        _ => throw new ArgumentOutOfRangeException(nameof(lenex), lenex, "Unknown value"),
    };

    private static Session ToSql(this SessionElement lenex) => new()
    {
        Date = DateOnly.FromDateTime(lenex.Date),
        StartTime = lenex.StartTime,
        EndTime = lenex.EndTime,
        Events = lenex.Events.Select(x => x.ToSql()).ToList(),
        Name = lenex.Name,
        Number = lenex.Number,
        OfficialMeeting = lenex.OfficialMeeting,
        WarmupStart = lenex.WarmupStart,
        WarmupEnd = lenex.WarmupEnd,
    };

    private static Event ToSql(this EventElement lenex) => new()
    {
        Id = lenex.EventId,
        AgeGroups = lenex.AgeGroups.Select(x => x.ToSql()).ToList(),
        Time = lenex.Time,
        Gender = lenex.Gender?.ToSql(),
        Number = lenex.Number,
        SwimStyle = lenex.SwimStyle.ToSql(),
    };

    private static Data.Sql.Gender ToSql(this Data.Lenex.Xml.Gender lenex) => lenex switch
    {
        Data.Lenex.Xml.Gender.All => Data.Sql.Gender.All,
        Data.Lenex.Xml.Gender.Female => Data.Sql.Gender.Female,
        Data.Lenex.Xml.Gender.Male => Data.Sql.Gender.Male,
        Data.Lenex.Xml.Gender.Mixed => Data.Sql.Gender.Mixed,
        _ => throw new ArgumentOutOfRangeException(nameof(lenex), lenex, "Unknown value"),
    };

    private static SwimStyle ToSql(this SwimStyleElement lenex) => new()
    {
        Distance = lenex.Distance,
        RelayCount = lenex.RelayCount,
        Stroke = lenex.Stroke.ToSql(),
    };

    private static Data.Sql.Stroke ToSql(this Data.Lenex.Xml.Stroke lenex) => lenex switch
    {
        Data.Lenex.Xml.Stroke.Apnea => Data.Sql.Stroke.Apnea,
        Data.Lenex.Xml.Stroke.Backstroke => Data.Sql.Stroke.Backstroke,
        Data.Lenex.Xml.Stroke.BiFins => Data.Sql.Stroke.BiFins,
        Data.Lenex.Xml.Stroke.Breaststroke => Data.Sql.Stroke.Breaststroke,
        Data.Lenex.Xml.Stroke.Fly => Data.Sql.Stroke.Fly,
        Data.Lenex.Xml.Stroke.Freestyle => Data.Sql.Stroke.Freestyle,
        Data.Lenex.Xml.Stroke.Immersion => Data.Sql.Stroke.Immersion,
        Data.Lenex.Xml.Stroke.IndividualMedleyRelay => Data.Sql.Stroke.IndividualMedleyRelay,
        Data.Lenex.Xml.Stroke.Medley => Data.Sql.Stroke.Medley,
        Data.Lenex.Xml.Stroke.Surface => Data.Sql.Stroke.Surface,
        Data.Lenex.Xml.Stroke.Unknown => Data.Sql.Stroke.Unknown,
        _ => throw new ArgumentOutOfRangeException(nameof(lenex), lenex, "Unknown value"),
    };

    private static AgeGroup ToSql(this AgeGroupElement lenex) => new()
    {
        MaxAge = lenex.MaxAge,
        MinAge = lenex.MinAge,
        Gender = lenex.Gender?.ToSql(),
        Rankings = lenex.Rankings.Select(x => x.ToSql()).ToList(),
    };

    private static Ranking ToSql(this RankingElement lenex) => new()
    {
        Order = lenex.Order,
        Place = lenex.Place,
        ResultId = lenex.ResultId,
    };

    private static Athlete ToSql(this AthleteElement lenex) => new()
    {
        Birthdate = DateOnly.FromDateTime(lenex.Birthdate),
        FirstName = lenex.FirstName,
        Gender = lenex.Gender.ToSql(),
        LastName = lenex.LastName,
        License = lenex.License,
        NamePrefix = lenex.NamePrefix,
        Results = lenex.Results.Select(x => x.ToSql()).ToList(),
        Entries = lenex.Entries.Select(x => x.ToSql()).ToList(),
    };

    private static Result ToSql(this ResultElement lenex) => new()
    {
        EventId = lenex.EventId,
        Lane = lenex.Lane,
        Id = lenex.ResultId,
        SwimTime = lenex.SwimTime,
        Status = lenex.Status?.ToSql(),
    };

    private static Entry ToSql(this EntryElement lenex) => new(Guid.NewGuid())
    {
        EventId = lenex.EventId,
        EntryTime = lenex.EntryTime,
    };

    private static Data.Sql.ResultStatus ToSql(this Data.Lenex.Xml.ResultStatus lenex) => lenex switch
    {
        Data.Lenex.Xml.ResultStatus.Exhibition => Data.Sql.ResultStatus.Exhibition,
        Data.Lenex.Xml.ResultStatus.Disqualified => Data.Sql.ResultStatus.Disqualified,
        Data.Lenex.Xml.ResultStatus.DidNotStart => Data.Sql.ResultStatus.DidNotStart,
        Data.Lenex.Xml.ResultStatus.DidNotFinish => Data.Sql.ResultStatus.DidNotFinish,
        Data.Lenex.Xml.ResultStatus.Sick => Data.Sql.ResultStatus.Sick,
        Data.Lenex.Xml.ResultStatus.Withdrawn => Data.Sql.ResultStatus.Withdrawn,
        _ => throw new ArgumentOutOfRangeException(nameof(lenex), lenex, "Unknown value"),
    };
}
