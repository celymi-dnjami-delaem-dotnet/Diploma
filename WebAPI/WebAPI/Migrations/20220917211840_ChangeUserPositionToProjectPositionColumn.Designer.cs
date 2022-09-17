﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebAPI.Infrastructure.Postgres;

namespace WebAPI.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20220917211840_ChangeUserPositionToProjectPositionColumn")]
    partial class ChangeUserPositionToProjectPositionColumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("WebAPI.Core.Entities.Epic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("EpicId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamptz");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("EpicDescription")
                        .HasColumnType("text");

                    b.Property<string>("EpicName")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Epics");
                });

            modelBuilder.Entity("WebAPI.Core.Entities.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamptz");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("ProjectDescription")
                        .HasColumnType("text");

                    b.Property<string>("ProjectName")
                        .HasColumnType("text");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("WorkSpaceId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("WorkSpaceId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("WebAPI.Core.Entities.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("RefreshTokenId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamptz");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("WebAPI.Core.Entities.Sprint", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("SprintId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamptz");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("EpicId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("SprintName")
                        .HasColumnType("text");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("EpicId");

                    b.ToTable("Sprints");
                });

            modelBuilder.Entity("WebAPI.Core.Entities.Story", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("StoryId")
                        .HasColumnType("uuid");

                    b.Property<string>("BlockReason")
                        .HasColumnType("text");

                    b.Property<int>("ColumnType")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamptz");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("Estimate")
                        .HasColumnType("integer");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsReady")
                        .HasColumnType("boolean");

                    b.Property<string>("Notes")
                        .HasColumnType("text");

                    b.Property<int>("ProjectPosition")
                        .HasColumnType("integer");

                    b.Property<uint>("RecordVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("xmin")
                        .HasColumnType("xid");

                    b.Property<Guid?>("SprintId")
                        .HasColumnType("uuid");

                    b.Property<int>("StoryPriority")
                        .HasColumnType("integer");

                    b.Property<Guid?>("TeamId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ColumnType");

                    b.HasIndex("SprintId");

                    b.HasIndex("TeamId");

                    b.HasIndex("Title");

                    b.HasIndex("UserId");

                    b.ToTable("Stories");
                });

            modelBuilder.Entity("WebAPI.Core.Entities.StoryHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("StoryHistoryId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamptz");

                    b.Property<string>("CurrentValue")
                        .HasColumnType("text");

                    b.Property<string>("FieldName")
                        .HasColumnType("text");

                    b.Property<string>("PreviousValue")
                        .HasColumnType("text");

                    b.Property<int>("StoryHistoryAction")
                        .HasColumnType("integer");

                    b.Property<Guid>("StoryId")
                        .HasColumnType("uuid");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("StoryId");

                    b.ToTable("StoryHistories");
                });

            modelBuilder.Entity("WebAPI.Core.Entities.Team", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("TeamId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Location")
                        .HasColumnType("text");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<string>("TeamName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("WebAPI.Core.Entities.TeamUser", b =>
                {
                    b.Property<Guid>("TeamId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("TeamId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("TeamUsers");
                });

            modelBuilder.Entity("WebAPI.Core.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("AvatarLink")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<int>("ProjectPosition")
                        .HasColumnType("integer");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.Property<int>("UserRole")
                        .HasColumnType("integer");

                    b.Property<Guid?>("WorkSpaceId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("Password");

                    b.HasIndex("ProjectPosition");

                    b.HasIndex("UserName");

                    b.HasIndex("UserRole");

                    b.HasIndex("WorkSpaceId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WebAPI.Core.Entities.WorkSpace", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("WorkSpaceId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamptz");

                    b.Property<string>("WorkSpaceDescription")
                        .HasColumnType("text");

                    b.Property<string>("WorkSpaceName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("WorkSpaces");
                });

            modelBuilder.Entity("WebAPI.Core.Entities.Epic", b =>
                {
                    b.HasOne("WebAPI.Core.Entities.Project", null)
                        .WithMany("Epics")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();
                });

            modelBuilder.Entity("WebAPI.Core.Entities.Project", b =>
                {
                    b.HasOne("WebAPI.Core.Entities.WorkSpace", null)
                        .WithMany("Projects")
                        .HasForeignKey("WorkSpaceId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();
                });

            modelBuilder.Entity("WebAPI.Core.Entities.RefreshToken", b =>
                {
                    b.HasOne("WebAPI.Core.Entities.User", null)
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();
                });

            modelBuilder.Entity("WebAPI.Core.Entities.Sprint", b =>
                {
                    b.HasOne("WebAPI.Core.Entities.Epic", null)
                        .WithMany("Sprints")
                        .HasForeignKey("EpicId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();
                });

            modelBuilder.Entity("WebAPI.Core.Entities.Story", b =>
                {
                    b.HasOne("WebAPI.Core.Entities.Sprint", null)
                        .WithMany("Stories")
                        .HasForeignKey("SprintId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("WebAPI.Core.Entities.Team", null)
                        .WithMany("Stories")
                        .HasForeignKey("TeamId");

                    b.HasOne("WebAPI.Core.Entities.User", null)
                        .WithMany("Stories")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("WebAPI.Core.Entities.StoryHistory", b =>
                {
                    b.HasOne("WebAPI.Core.Entities.Story", null)
                        .WithMany("StoryHistories")
                        .HasForeignKey("StoryId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();
                });

            modelBuilder.Entity("WebAPI.Core.Entities.Team", b =>
                {
                    b.HasOne("WebAPI.Core.Entities.Project", null)
                        .WithMany("Teams")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("WebAPI.Core.Entities.TeamUser", b =>
                {
                    b.HasOne("WebAPI.Core.Entities.Team", "Team")
                        .WithMany("TeamUsers")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebAPI.Core.Entities.User", "User")
                        .WithMany("TeamUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebAPI.Core.Entities.User", b =>
                {
                    b.HasOne("WebAPI.Core.Entities.WorkSpace", null)
                        .WithMany("Users")
                        .HasForeignKey("WorkSpaceId")
                        .OnDelete(DeleteBehavior.SetNull);
                });
#pragma warning restore 612, 618
        }
    }
}
