﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ZenDev.Persistence;

#nullable disable

namespace ZenDev.Persistence.Migrations
{
    [DbContext(typeof(ZenDevDbContext))]
    partial class ZenDevDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ZenDev.Persistence.Entities.ExampleEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Examples");
                });

            modelBuilder.Entity("ZenDev.Persistence.Entities.ExerciseEntity", b =>
                {
                    b.Property<long>("ExerciseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ExerciseId"));

                    b.Property<string>("ExerciseName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ExerciseId");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("ZenDev.Persistence.Entities.ExerciseTypeEntity", b =>
                {
                    b.Property<long>("ExerciseTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ExerciseTypeId"));

                    b.Property<string>("ExerciseType")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ExerciseTypeId");

                    b.ToTable("ExerciseTypes");
                });

            modelBuilder.Entity("ZenDev.Persistence.Entities.GroupEntity", b =>
                {
                    b.Property<long>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("GroupId"));

                    b.Property<long>("ExerciseTypeId")
                        .HasColumnType("bigint");

                    b.Property<string>("GroupIconUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("GroupId");

                    b.HasIndex("ExerciseTypeId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("ZenDev.Persistence.Entities.PersonalGoalEntity", b =>
                {
                    b.Property<long>("GoalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("GoalId"));

                    b.Property<long>("AmountCompleted")
                        .HasColumnType("bigint");

                    b.Property<long>("AmountToComplete")
                        .HasColumnType("bigint");

                    b.Property<long>("ExerciseId")
                        .HasColumnType("bigint");

                    b.Property<string>("GoalDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("GoalEndDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("GoalName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("GoalStartDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("MeasurementUnit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("GoalId");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("UserId");

                    b.ToTable("PersonalGoals");
                });

            modelBuilder.Entity("ZenDev.Persistence.Entities.UserEntity", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("UserId"));

                    b.Property<string>("AvatarIconUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("LastActive")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("StravaRefreshToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Streak")
                        .HasColumnType("bigint");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ZenDev.Persistence.Entities.UserGroupBridgeEntity", b =>
                {
                    b.Property<long>("UserGroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("UserGroupId"));

                    b.Property<long>("GroupId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("UserGroupId");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("UserGroupBridge");
                });

            modelBuilder.Entity("ZenDev.Persistence.Entities.GroupEntity", b =>
                {
                    b.HasOne("ZenDev.Persistence.Entities.ExerciseTypeEntity", "ExerciseTypeEntity")
                        .WithMany()
                        .HasForeignKey("ExerciseTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExerciseTypeEntity");
                });

            modelBuilder.Entity("ZenDev.Persistence.Entities.PersonalGoalEntity", b =>
                {
                    b.HasOne("ZenDev.Persistence.Entities.ExerciseEntity", "ExerciseEntity")
                        .WithMany()
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ZenDev.Persistence.Entities.UserEntity", "UserEntity")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExerciseEntity");

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("ZenDev.Persistence.Entities.UserGroupBridgeEntity", b =>
                {
                    b.HasOne("ZenDev.Persistence.Entities.GroupEntity", "GroupEntity")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ZenDev.Persistence.Entities.UserEntity", "UserEntity")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GroupEntity");

                    b.Navigation("UserEntity");
                });
#pragma warning restore 612, 618
        }
    }
}
