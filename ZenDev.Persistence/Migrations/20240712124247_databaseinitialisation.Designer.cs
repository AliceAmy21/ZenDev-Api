﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ZenDev.Persistence;

#nullable disable

namespace ZenDev.Persistence.Migrations
{
    [DbContext(typeof(ZenDevDbContext))]
<<<<<<<< HEAD:ZenDev.Persistence/Migrations/20240712124247_databaseinitialisation.Designer.cs
    [Migration("20240712124247_databaseinitialisation")]
========
    [Migration("20240717124755_databaseinitialisation")]
>>>>>>>> features/9-achievements:ZenDev.Persistence/Migrations/20240717124755_databaseinitialisation.Designer.cs
    partial class databaseinitialisation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ZenDev.Persistence.Entities.AchievementEntity", b =>
                {
                    b.Property<long>("AchievementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("AchievementId"));

                    b.Property<string>("AchievementDescription")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("AchievementIcon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AchievementName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("AchievementId");

                    b.ToTable("Achievements");
                });

            modelBuilder.Entity("ZenDev.Persistence.Entities.ActivityRecordEntity", b =>
                {
                    b.Property<long>("ActivityRecordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ActivityRecordId"));

                    b.Property<DateTimeOffset>("DateTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<long>("Distance")
                        .HasColumnType("bigint");

                    b.Property<long>("Duration")
                        .HasColumnType("bigint");

                    b.Property<long>("Points")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("ActivityRecordId");

                    b.HasIndex("UserId");

                    b.ToTable("ActivityRecords");
                });

            modelBuilder.Entity("ZenDev.Persistence.Entities.ChallengeEntity", b =>
                {
                    b.Property<long>("ChallengeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ChallengeId"));

                    b.Property<long>("Admin")
                        .HasColumnType("bigint");

                    b.Property<long>("AmountToComplete")
                        .HasColumnType("bigint");

                    b.Property<string>("ChallengeDescription")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTimeOffset>("ChallengeEndDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("ChallengeName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset>("ChallengeStartDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<long>("ExerciseId")
                        .HasColumnType("bigint");

                    b.Property<long>("GroupId")
                        .HasColumnType("bigint");

                    b.Property<int>("Measurement")
                        .HasColumnType("int");

                    b.HasKey("ChallengeId");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("GroupId");

                    b.ToTable("Challenges");
                });

            modelBuilder.Entity("ZenDev.Persistence.Entities.ChatMessageBridgeEntity", b =>
                {
                    b.Property<long>("ChatMessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ChatMessageId"));

                    b.Property<long>("ChatId")
                        .HasColumnType("bigint");

                    b.Property<long>("MessageId")
                        .HasColumnType("bigint");

                    b.HasKey("ChatMessageId");

                    b.HasIndex("ChatId");

                    b.HasIndex("MessageId");

                    b.ToTable("ChatMessageBridge");
                });

            modelBuilder.Entity("ZenDev.Persistence.Entities.ChatroomEntity", b =>
                {
                    b.Property<long>("ChatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ChatId"));

                    b.Property<long>("GroupId")
                        .HasColumnType("bigint");

                    b.HasKey("ChatId");

                    b.HasIndex("GroupId");

                    b.ToTable("Chatrooms");
                });

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

                    b.Property<string>("GroupDescription")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("GroupIconUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<long>("MemberCount")
                        .HasColumnType("bigint");

                    b.HasKey("GroupId");

                    b.HasIndex("ExerciseTypeId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("ZenDev.Persistence.Entities.GroupInvitationEntity", b =>
                {
                    b.Property<long>("GroupInvitationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("GroupInvitationId"));

                    b.Property<long>("GroupId")
                        .HasColumnType("bigint");

                    b.Property<long>("InviteSenderId")
                        .HasColumnType("bigint");

                    b.Property<long>("InvitedUserId")
                        .HasColumnType("bigint");

                    b.HasKey("GroupInvitationId");

                    b.HasIndex("GroupId");

                    b.HasIndex("InvitedUserId");

                    b.ToTable("GroupInvitations");
                });

            modelBuilder.Entity("ZenDev.Persistence.Entities.MessageEntity", b =>
                {
                    b.Property<long>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("MessageId"));

                    b.Property<DateTimeOffset>("DateSent")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("MessageContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Shareable")
                        .HasColumnType("bit");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("MessageId");

                    b.HasIndex("UserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("ZenDev.Persistence.Entities.MessageReactionBridgeEntity", b =>
                {
                    b.Property<long>("MessageReactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("MessageReactionId"));

                    b.Property<long>("MessageId")
                        .HasColumnType("bigint");

                    b.Property<long>("ReactionId")
                        .HasColumnType("bigint");

                    b.HasKey("MessageReactionId");

                    b.HasIndex("MessageId");

                    b.HasIndex("ReactionId")
                        .IsUnique();

                    b.ToTable("MessageReactionBridge");
                });

            modelBuilder.Entity("ZenDev.Persistence.Entities.MindfulnessEntity", b =>
                {
                    b.Property<long>("MindfulnessId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("MindfulnessId"));

                    b.Property<DateTimeOffset?>("LastUpdate")
                        .HasColumnType("datetimeoffset");

                    b.Property<double>("TodaysMinutes")
                        .HasColumnType("float");

                    b.Property<long>("TodaysPoints")
                        .HasColumnType("bigint");

                    b.Property<double>("TotalMinutes")
                        .HasColumnType("float");

                    b.Property<long>("TotalPoints")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("MindfulnessId");

                    b.HasIndex("UserId");

                    b.ToTable("Mindfulness");
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

            modelBuilder.Entity("ZenDev.Persistence.Entities.ReactionEntity", b =>
                {
                    b.Property<long>("ReactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ReactionId"));

                    b.Property<string>("Reaction")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("ReactionId");

                    b.HasIndex("UserId");

                    b.ToTable("Reactions");
                });

            modelBuilder.Entity("ZenDev.Persistence.Entities.UserAchievementBridgeEntity", b =>
                {
                    b.Property<long>("UserAchievementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("UserAchievementId"));

                    b.Property<long>("AchievementId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("UserAchievementId");

                    b.HasIndex("AchievementId");

                    b.HasIndex("UserId");

                    b.ToTable("UserAchievementBridge");
                });

            modelBuilder.Entity("ZenDev.Persistence.Entities.UserChallengeBridgeEntity", b =>
                {
                    b.Property<long>("UserChallengeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("UserChallengeId"));

                    b.Property<long>("AmountCompleted")
                        .HasColumnType("bigint");

                    b.Property<long>("ChallengeId")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("DateCompleted")
                        .HasColumnType("datetimeoffset");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("UserChallengeId");

                    b.HasIndex("ChallengeId");

                    b.HasIndex("UserId");

                    b.ToTable("UserChallengeBridge");
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

                    b.Property<DateTimeOffset?>("LastSynced")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("StravaRefreshToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Streak")
                        .HasColumnType("bigint");

                    b.Property<long>("TotalPoints")
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

                    b.Property<bool>("GroupAdmin")
                        .HasColumnType("bit");

                    b.Property<long>("GroupId")
                        .HasColumnType("bigint");

                    b.Property<long>("Points")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("UserGroupId");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("UserGroupBridge");
                });

            modelBuilder.Entity("ZenDev.Persistence.Entities.ActivityRecordEntity", b =>
                {
                    b.HasOne("ZenDev.Persistence.Entities.UserEntity", "UserEntities")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserEntities");
                });

            modelBuilder.Entity("ZenDev.Persistence.Entities.ChallengeEntity", b =>
                {
                    b.HasOne("ZenDev.Persistence.Entities.ExerciseEntity", "ExerciseEntity")
                        .WithMany()
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ZenDev.Persistence.Entities.GroupEntity", "GroupEntity")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExerciseEntity");

                    b.Navigation("GroupEntity");
                });

            modelBuilder.Entity("ZenDev.Persistence.Entities.ChatMessageBridgeEntity", b =>
                {
                    b.HasOne("ZenDev.Persistence.Entities.ChatroomEntity", "ChatroomEntity")
                        .WithMany("ChatMessageBridges")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ZenDev.Persistence.Entities.MessageEntity", "MessageEntity")
                        .WithMany("ChatMessageBridges")
                        .HasForeignKey("MessageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChatroomEntity");

                    b.Navigation("MessageEntity");
                });

            modelBuilder.Entity("ZenDev.Persistence.Entities.ChatroomEntity", b =>
                {
                    b.HasOne("ZenDev.Persistence.Entities.GroupEntity", "GroupEntity")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GroupEntity");
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

            modelBuilder.Entity("ZenDev.Persistence.Entities.GroupInvitationEntity", b =>
                {
                    b.HasOne("ZenDev.Persistence.Entities.GroupEntity", "GroupEntity")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ZenDev.Persistence.Entities.UserEntity", "UserEntity")
                        .WithMany()
                        .HasForeignKey("InvitedUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GroupEntity");

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("ZenDev.Persistence.Entities.MessageEntity", b =>
                {
                    b.HasOne("ZenDev.Persistence.Entities.UserEntity", "UserEntity")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("ZenDev.Persistence.Entities.MessageReactionBridgeEntity", b =>
                {
                    b.HasOne("ZenDev.Persistence.Entities.MessageEntity", "MessageEntity")
                        .WithMany("messageReactionBridges")
                        .HasForeignKey("MessageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ZenDev.Persistence.Entities.ReactionEntity", "ReactionEntity")
                        .WithOne("MessageReactionBridgeEntity")
                        .HasForeignKey("ZenDev.Persistence.Entities.MessageReactionBridgeEntity", "ReactionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("MessageEntity");

                    b.Navigation("ReactionEntity");
                });

            modelBuilder.Entity("ZenDev.Persistence.Entities.MindfulnessEntity", b =>
                {
                    b.HasOne("ZenDev.Persistence.Entities.UserEntity", "UserEntity")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserEntity");
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

            modelBuilder.Entity("ZenDev.Persistence.Entities.ReactionEntity", b =>
                {
                    b.HasOne("ZenDev.Persistence.Entities.UserEntity", "UserEntity")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("ZenDev.Persistence.Entities.UserAchievementBridgeEntity", b =>
                {
                    b.HasOne("ZenDev.Persistence.Entities.AchievementEntity", "AchievementEntity")
                        .WithMany()
                        .HasForeignKey("AchievementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ZenDev.Persistence.Entities.UserEntity", "UserEntity")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AchievementEntity");

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("ZenDev.Persistence.Entities.UserChallengeBridgeEntity", b =>
                {
                    b.HasOne("ZenDev.Persistence.Entities.ChallengeEntity", "ChallengeEntity")
                        .WithMany("UserChallengeBridgeEntities")
                        .HasForeignKey("ChallengeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ZenDev.Persistence.Entities.UserEntity", "UserEntity")
                        .WithMany("UserChallengeBridgeEntities")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChallengeEntity");

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("ZenDev.Persistence.Entities.UserGroupBridgeEntity", b =>
                {
                    b.HasOne("ZenDev.Persistence.Entities.GroupEntity", "GroupEntity")
                        .WithMany("UserGroupBridgeEntities")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ZenDev.Persistence.Entities.UserEntity", "UserEntity")
                        .WithMany("UserGroupBridgeEntities")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GroupEntity");

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("ZenDev.Persistence.Entities.ChallengeEntity", b =>
                {
                    b.Navigation("UserChallengeBridgeEntities");
                });

            modelBuilder.Entity("ZenDev.Persistence.Entities.ChatroomEntity", b =>
                {
                    b.Navigation("ChatMessageBridges");
                });

            modelBuilder.Entity("ZenDev.Persistence.Entities.GroupEntity", b =>
                {
                    b.Navigation("UserGroupBridgeEntities");
                });

            modelBuilder.Entity("ZenDev.Persistence.Entities.MessageEntity", b =>
                {
                    b.Navigation("ChatMessageBridges");

                    b.Navigation("messageReactionBridges");
                });

            modelBuilder.Entity("ZenDev.Persistence.Entities.ReactionEntity", b =>
                {
                    b.Navigation("MessageReactionBridgeEntity")
                        .IsRequired();
                });

            modelBuilder.Entity("ZenDev.Persistence.Entities.UserEntity", b =>
                {
                    b.Navigation("UserChallengeBridgeEntities");

                    b.Navigation("UserGroupBridgeEntities");
                });
#pragma warning restore 612, 618
        }
    }
}
