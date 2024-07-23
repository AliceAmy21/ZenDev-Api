using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZenDev.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class databaseinitialisation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    AchievementId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AchievementName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AchievementDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AchievementIcon = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.AchievementId);
                });

            migrationBuilder.CreateTable(
                name: "Examples",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examples", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    ExerciseId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExerciseName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.ExerciseId);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseTypes",
                columns: table => new
                {
                    ExerciseTypeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExerciseType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseTypes", x => x.ExerciseTypeId);
                });

            migrationBuilder.CreateTable(
                name: "TournamentGroups",
                columns: table => new
                {
                    TGroupId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TGroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TGroupDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TGroupIconUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemberCount = table.Column<long>(type: "bigint", nullable: false),
                    ExerciseName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentGroups", x => x.TGroupId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserEmail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StravaRefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Streak = table.Column<long>(type: "bigint", nullable: false),
                    AvatarIconUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastActive = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastSynced = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    TotalPoints = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Tournaments",
                columns: table => new
                {
                    TournamentId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TournamentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TournamentDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ExerciseId = table.Column<long>(type: "bigint", nullable: false),
                    StartDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EndDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.TournamentId);
                    table.ForeignKey(
                        name: "FK_Tournaments_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "ExerciseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    GroupId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GroupDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    GroupIconUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExerciseTypeId = table.Column<long>(type: "bigint", nullable: false),
                    MemberCount = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.GroupId);
                    table.ForeignKey(
                        name: "FK_Groups_ExerciseTypes_ExerciseTypeId",
                        column: x => x.ExerciseTypeId,
                        principalTable: "ExerciseTypes",
                        principalColumn: "ExerciseTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActivityRecords",
                columns: table => new
                {
                    ActivityRecordId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Points = table.Column<long>(type: "bigint", nullable: false),
                    Distance = table.Column<long>(type: "bigint", nullable: false),
                    Duration = table.Column<long>(type: "bigint", nullable: false),
                    DateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityRecords", x => x.ActivityRecordId);
                    table.ForeignKey(
                        name: "FK_ActivityRecords_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessageId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    MessageContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateSent = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Shareable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_Messages_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mindfulness",
                columns: table => new
                {
                    MindfulnessId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalPoints = table.Column<long>(type: "bigint", nullable: false),
                    TodaysPoints = table.Column<long>(type: "bigint", nullable: false),
                    TotalMinutes = table.Column<double>(type: "float", nullable: false),
                    TodaysMinutes = table.Column<double>(type: "float", nullable: false),
                    LastUpdate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mindfulness", x => x.MindfulnessId);
                    table.ForeignKey(
                        name: "FK_Mindfulness_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalGoals",
                columns: table => new
                {
                    GoalId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GoalName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GoalDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GoalStartDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    GoalEndDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    AmountToComplete = table.Column<long>(type: "bigint", nullable: false),
                    AmountCompleted = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ExerciseId = table.Column<long>(type: "bigint", nullable: false),
                    MeasurementUnit = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalGoals", x => x.GoalId);
                    table.ForeignKey(
                        name: "FK_PersonalGoals_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "ExerciseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonalGoals_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reactions",
                columns: table => new
                {
                    ReactionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Reaction = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reactions", x => x.ReactionId);
                    table.ForeignKey(
                        name: "FK_Reactions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TournamentGroupUserBridge",
                columns: table => new
                {
                    TournamentGroupUserId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TGroupId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentGroupUserBridge", x => x.TournamentGroupUserId);
                    table.ForeignKey(
                        name: "FK_TournamentGroupUserBridge_TournamentGroups_TGroupId",
                        column: x => x.TGroupId,
                        principalTable: "TournamentGroups",
                        principalColumn: "TGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TournamentGroupUserBridge_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAchievementBridge",
                columns: table => new
                {
                    UserAchievementId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    AchievementId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAchievementBridge", x => x.UserAchievementId);
                    table.ForeignKey(
                        name: "FK_UserAchievementBridge_Achievements_AchievementId",
                        column: x => x.AchievementId,
                        principalTable: "Achievements",
                        principalColumn: "AchievementId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAchievementBridge_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TournamentGroupBridge",
                columns: table => new
                {
                    TournamentGroupId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TournamentId = table.Column<long>(type: "bigint", nullable: false),
                    TGroupId = table.Column<long>(type: "bigint", nullable: false),
                    Points = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentGroupBridge", x => x.TournamentGroupId);
                    table.ForeignKey(
                        name: "FK_TournamentGroupBridge_TournamentGroups_TGroupId",
                        column: x => x.TGroupId,
                        principalTable: "TournamentGroups",
                        principalColumn: "TGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TournamentGroupBridge_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "TournamentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Challenges",
                columns: table => new
                {
                    ChallengeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChallengeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ChallengeDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ChallengeStartDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ChallengeEndDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Measurement = table.Column<int>(type: "int", nullable: false),
                    AmountToComplete = table.Column<long>(type: "bigint", nullable: false),
                    ExerciseId = table.Column<long>(type: "bigint", nullable: false),
                    GroupId = table.Column<long>(type: "bigint", nullable: false),
                    Admin = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Challenges", x => x.ChallengeId);
                    table.ForeignKey(
                        name: "FK_Challenges_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "ExerciseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Challenges_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chatrooms",
                columns: table => new
                {
                    ChatId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chatrooms", x => x.ChatId);
                    table.ForeignKey(
                        name: "FK_Chatrooms_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupInvitations",
                columns: table => new
                {
                    GroupInvitationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<long>(type: "bigint", nullable: false),
                    InvitedUserId = table.Column<long>(type: "bigint", nullable: false),
                    InviteSenderId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupInvitations", x => x.GroupInvitationId);
                    table.ForeignKey(
                        name: "FK_GroupInvitations_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupInvitations_Users_InvitedUserId",
                        column: x => x.InvitedUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserGroupBridge",
                columns: table => new
                {
                    UserGroupId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupAdmin = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    GroupId = table.Column<long>(type: "bigint", nullable: false),
                    Points = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroupBridge", x => x.UserGroupId);
                    table.ForeignKey(
                        name: "FK_UserGroupBridge_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGroupBridge_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MessageReactionBridge",
                columns: table => new
                {
                    MessageReactionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReactionId = table.Column<long>(type: "bigint", nullable: false),
                    MessageId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageReactionBridge", x => x.MessageReactionId);
                    table.ForeignKey(
                        name: "FK_MessageReactionBridge_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "MessageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessageReactionBridge_Reactions_ReactionId",
                        column: x => x.ReactionId,
                        principalTable: "Reactions",
                        principalColumn: "ReactionId");
                });

            migrationBuilder.CreateTable(
                name: "UserChallengeBridge",
                columns: table => new
                {
                    UserChallengeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ChallengeId = table.Column<long>(type: "bigint", nullable: false),
                    DateCompleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    AmountCompleted = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChallengeBridge", x => x.UserChallengeId);
                    table.ForeignKey(
                        name: "FK_UserChallengeBridge_Challenges_ChallengeId",
                        column: x => x.ChallengeId,
                        principalTable: "Challenges",
                        principalColumn: "ChallengeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserChallengeBridge_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessageBridge",
                columns: table => new
                {
                    ChatMessageId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatId = table.Column<long>(type: "bigint", nullable: false),
                    MessageId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessageBridge", x => x.ChatMessageId);
                    table.ForeignKey(
                        name: "FK_ChatMessageBridge_Chatrooms_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chatrooms",
                        principalColumn: "ChatId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatMessageBridge_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "MessageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityRecords_UserId",
                table: "ActivityRecords",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Challenges_ExerciseId",
                table: "Challenges",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_Challenges_GroupId",
                table: "Challenges",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessageBridge_ChatId",
                table: "ChatMessageBridge",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessageBridge_MessageId",
                table: "ChatMessageBridge",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_Chatrooms_GroupId",
                table: "Chatrooms",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupInvitations_GroupId",
                table: "GroupInvitations",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupInvitations_InvitedUserId",
                table: "GroupInvitations",
                column: "InvitedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_ExerciseTypeId",
                table: "Groups",
                column: "ExerciseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageReactionBridge_MessageId",
                table: "MessageReactionBridge",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageReactionBridge_ReactionId",
                table: "MessageReactionBridge",
                column: "ReactionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserId",
                table: "Messages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Mindfulness_UserId",
                table: "Mindfulness",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalGoals_ExerciseId",
                table: "PersonalGoals",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalGoals_UserId",
                table: "PersonalGoals",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_UserId",
                table: "Reactions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentGroupBridge_TGroupId",
                table: "TournamentGroupBridge",
                column: "TGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentGroupBridge_TournamentId",
                table: "TournamentGroupBridge",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentGroupUserBridge_TGroupId",
                table: "TournamentGroupUserBridge",
                column: "TGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentGroupUserBridge_UserId",
                table: "TournamentGroupUserBridge",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_ExerciseId",
                table: "Tournaments",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAchievementBridge_AchievementId",
                table: "UserAchievementBridge",
                column: "AchievementId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAchievementBridge_UserId",
                table: "UserAchievementBridge",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChallengeBridge_ChallengeId",
                table: "UserChallengeBridge",
                column: "ChallengeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChallengeBridge_UserId",
                table: "UserChallengeBridge",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupBridge_GroupId",
                table: "UserGroupBridge",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupBridge_UserId",
                table: "UserGroupBridge",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityRecords");

            migrationBuilder.DropTable(
                name: "ChatMessageBridge");

            migrationBuilder.DropTable(
                name: "Examples");

            migrationBuilder.DropTable(
                name: "GroupInvitations");

            migrationBuilder.DropTable(
                name: "MessageReactionBridge");

            migrationBuilder.DropTable(
                name: "Mindfulness");

            migrationBuilder.DropTable(
                name: "PersonalGoals");

            migrationBuilder.DropTable(
                name: "TournamentGroupBridge");

            migrationBuilder.DropTable(
                name: "TournamentGroupUserBridge");

            migrationBuilder.DropTable(
                name: "UserAchievementBridge");

            migrationBuilder.DropTable(
                name: "UserChallengeBridge");

            migrationBuilder.DropTable(
                name: "UserGroupBridge");

            migrationBuilder.DropTable(
                name: "Chatrooms");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Reactions");

            migrationBuilder.DropTable(
                name: "Tournaments");

            migrationBuilder.DropTable(
                name: "TournamentGroups");

            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropTable(
                name: "Challenges");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "ExerciseTypes");
        }
    }
}
