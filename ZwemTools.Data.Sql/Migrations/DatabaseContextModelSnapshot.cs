﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ZwemTools.Data.Sql;

#nullable disable

namespace ZwemTools.Data.Sql.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true);

            modelBuilder.Entity("AthleteTeam", b =>
                {
                    b.Property<int>("AthletesId")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("SplashQuestTeamsId")
                        .HasColumnType("TEXT");

                    b.HasKey("AthletesId", "SplashQuestTeamsId");

                    b.HasIndex("SplashQuestTeamsId");

                    b.ToTable("AthleteTeam");
                });

            modelBuilder.Entity("ZwemTools.Data.Sql.AgeGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EventId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Gender")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MaxAge")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MinAge")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("AgeGroups");
                });

            modelBuilder.Entity("ZwemTools.Data.Sql.Athlete", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly>("Birthdate")
                        .HasColumnType("TEXT");

                    b.Property<int>("ClubId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Gender")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("License")
                        .HasColumnType("TEXT");

                    b.Property<string>("NamePrefix")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ClubId");

                    b.ToTable("Athletes");
                });

            modelBuilder.Entity("ZwemTools.Data.Sql.Club", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("MeetId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("MeetId");

                    b.ToTable("Clubs");
                });

            modelBuilder.Entity("ZwemTools.Data.Sql.Entry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("AthleteId")
                        .HasColumnType("INTEGER");

                    b.Property<TimeSpan?>("EntryTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("EventId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AthleteId");

                    b.HasIndex("EventId");

                    b.ToTable("Entries");
                });

            modelBuilder.Entity("ZwemTools.Data.Sql.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Gender")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Number")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SessionId")
                        .HasColumnType("INTEGER");

                    b.Property<TimeOnly?>("Time")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SessionId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("ZwemTools.Data.Sql.Meet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("Course")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LiveTiming")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nation")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Organizer")
                        .HasColumnType("TEXT");

                    b.Property<string>("OrganizerUrl")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Meets");
                });

            modelBuilder.Entity("ZwemTools.Data.Sql.Ranking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AgeGroupId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Order")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Place")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ResultId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AgeGroupId");

                    b.HasIndex("ResultId");

                    b.ToTable("Rankings");
                });

            modelBuilder.Entity("ZwemTools.Data.Sql.Result", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AthleteId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EventId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Lane")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<TimeSpan>("SwimTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AthleteId");

                    b.HasIndex("EventId");

                    b.ToTable("Results");
                });

            modelBuilder.Entity("ZwemTools.Data.Sql.Session", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("TEXT");

                    b.Property<TimeOnly?>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("MeetId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("Number")
                        .HasColumnType("INTEGER");

                    b.Property<TimeOnly?>("OfficialMeeting")
                        .HasColumnType("TEXT");

                    b.Property<TimeOnly?>("StartTime")
                        .HasColumnType("TEXT");

                    b.Property<TimeOnly?>("WarmupEnd")
                        .HasColumnType("TEXT");

                    b.Property<TimeOnly?>("WarmupStart")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("MeetId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("ZwemTools.Data.Sql.SplashQuest.SplashQuestMeet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("MeetId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("MeetId");

                    b.ToTable("SplashQuestMeets");
                });

            modelBuilder.Entity("ZwemTools.Data.Sql.SplashQuest.Team", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("SplashQuestMeetId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SplashQuestMeetId");

                    b.ToTable("SplashQuestTeams");
                });

            modelBuilder.Entity("AthleteTeam", b =>
                {
                    b.HasOne("ZwemTools.Data.Sql.Athlete", null)
                        .WithMany()
                        .HasForeignKey("AthletesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ZwemTools.Data.Sql.SplashQuest.Team", null)
                        .WithMany()
                        .HasForeignKey("SplashQuestTeamsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ZwemTools.Data.Sql.AgeGroup", b =>
                {
                    b.HasOne("ZwemTools.Data.Sql.Event", "Event")
                        .WithMany("AgeGroups")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("ZwemTools.Data.Sql.Athlete", b =>
                {
                    b.HasOne("ZwemTools.Data.Sql.Club", "Club")
                        .WithMany("Athletes")
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Club");
                });

            modelBuilder.Entity("ZwemTools.Data.Sql.Club", b =>
                {
                    b.HasOne("ZwemTools.Data.Sql.Meet", "Meet")
                        .WithMany("Clubs")
                        .HasForeignKey("MeetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Meet");
                });

            modelBuilder.Entity("ZwemTools.Data.Sql.Entry", b =>
                {
                    b.HasOne("ZwemTools.Data.Sql.Athlete", "Athlete")
                        .WithMany("Entries")
                        .HasForeignKey("AthleteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ZwemTools.Data.Sql.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Athlete");

                    b.Navigation("Event");
                });

            modelBuilder.Entity("ZwemTools.Data.Sql.Event", b =>
                {
                    b.HasOne("ZwemTools.Data.Sql.Session", "Session")
                        .WithMany("Events")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("ZwemTools.Data.Sql.SwimStyle", "SwimStyle", b1 =>
                        {
                            b1.Property<int>("EventId")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Distance")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("RelayCount")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Stroke")
                                .HasColumnType("INTEGER");

                            b1.HasKey("EventId");

                            b1.ToTable("Events");

                            b1.WithOwner()
                                .HasForeignKey("EventId");
                        });

                    b.Navigation("Session");

                    b.Navigation("SwimStyle")
                        .IsRequired();
                });

            modelBuilder.Entity("ZwemTools.Data.Sql.Meet", b =>
                {
                    b.OwnsOne("ZwemTools.Data.Sql.AgeDate", "AgeDate", b1 =>
                        {
                            b1.Property<int>("MeetId")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.Property<DateOnly>("Value")
                                .HasColumnType("TEXT");

                            b1.HasKey("MeetId");

                            b1.ToTable("Meets");

                            b1.WithOwner()
                                .HasForeignKey("MeetId");
                        });

                    b.Navigation("AgeDate");
                });

            modelBuilder.Entity("ZwemTools.Data.Sql.Ranking", b =>
                {
                    b.HasOne("ZwemTools.Data.Sql.AgeGroup", null)
                        .WithMany("Rankings")
                        .HasForeignKey("AgeGroupId");

                    b.HasOne("ZwemTools.Data.Sql.Result", "Result")
                        .WithMany()
                        .HasForeignKey("ResultId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Result");
                });

            modelBuilder.Entity("ZwemTools.Data.Sql.Result", b =>
                {
                    b.HasOne("ZwemTools.Data.Sql.Athlete", "Athlete")
                        .WithMany("Results")
                        .HasForeignKey("AthleteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ZwemTools.Data.Sql.Event", "Event")
                        .WithMany("Results")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Athlete");

                    b.Navigation("Event");
                });

            modelBuilder.Entity("ZwemTools.Data.Sql.Session", b =>
                {
                    b.HasOne("ZwemTools.Data.Sql.Meet", "Meet")
                        .WithMany("Sessions")
                        .HasForeignKey("MeetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Meet");
                });

            modelBuilder.Entity("ZwemTools.Data.Sql.SplashQuest.SplashQuestMeet", b =>
                {
                    b.HasOne("ZwemTools.Data.Sql.Meet", "Meet")
                        .WithMany()
                        .HasForeignKey("MeetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Meet");
                });

            modelBuilder.Entity("ZwemTools.Data.Sql.SplashQuest.Team", b =>
                {
                    b.HasOne("ZwemTools.Data.Sql.SplashQuest.SplashQuestMeet", null)
                        .WithMany("Teams")
                        .HasForeignKey("SplashQuestMeetId");

                    b.OwnsMany("ZwemTools.Data.Sql.SplashQuest.Assignment", "Assignments", b1 =>
                        {
                            b1.Property<Guid>("TeamId")
                                .HasColumnType("TEXT");

                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("TEXT");

                            b1.Property<int>("AthleteId")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Stroke")
                                .HasColumnType("INTEGER");

                            b1.HasKey("TeamId", "Id");

                            b1.HasIndex("AthleteId");

                            b1.HasIndex("Stroke", "AthleteId")
                                .IsUnique();

                            b1.ToTable("Assignment");

                            b1.HasOne("ZwemTools.Data.Sql.Athlete", "Athlete")
                                .WithMany()
                                .HasForeignKey("AthleteId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("TeamId");

                            b1.Navigation("Athlete");
                        });

                    b.Navigation("Assignments");
                });

            modelBuilder.Entity("ZwemTools.Data.Sql.AgeGroup", b =>
                {
                    b.Navigation("Rankings");
                });

            modelBuilder.Entity("ZwemTools.Data.Sql.Athlete", b =>
                {
                    b.Navigation("Entries");

                    b.Navigation("Results");
                });

            modelBuilder.Entity("ZwemTools.Data.Sql.Club", b =>
                {
                    b.Navigation("Athletes");
                });

            modelBuilder.Entity("ZwemTools.Data.Sql.Event", b =>
                {
                    b.Navigation("AgeGroups");

                    b.Navigation("Results");
                });

            modelBuilder.Entity("ZwemTools.Data.Sql.Meet", b =>
                {
                    b.Navigation("Clubs");

                    b.Navigation("Sessions");
                });

            modelBuilder.Entity("ZwemTools.Data.Sql.Session", b =>
                {
                    b.Navigation("Events");
                });

            modelBuilder.Entity("ZwemTools.Data.Sql.SplashQuest.SplashQuestMeet", b =>
                {
                    b.Navigation("Teams");
                });
#pragma warning restore 612, 618
        }
    }
}
