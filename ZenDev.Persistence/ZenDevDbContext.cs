using Microsoft.EntityFrameworkCore;
using ZenDev.Persistence.Entities;

namespace ZenDev.Persistence
{
    public class ZenDevDbContext : DbContext
    {
        private readonly bool _overrideConnectionString = false;
        private readonly string _connectionString = string.Empty;

        public ZenDevDbContext()
            : base()
        {
            _overrideConnectionString = true;
            _connectionString = Constants.ConnectionString.connectionString;
        }

        public ZenDevDbContext(string connectionString)
            : base()
        {
            if (string.IsNullOrEmpty(connectionString)) return;
            _overrideConnectionString = true;
            _connectionString = connectionString;
        }

        public ZenDevDbContext(DbContextOptions<ZenDevDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ExampleEntity> Examples { get; set; }
        public virtual DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<ExerciseEntity> Exercises { get; set; }
        public virtual DbSet<ExerciseTypeEntity> ExerciseTypes { get; set; }
        public virtual DbSet<GroupEntity> Groups { get; set; }
        public virtual DbSet<GroupInvitationEntity> GroupInvitations { get; set; }
        public virtual DbSet<UserGroupBridgeEntity> UserGroupBridge { get; set; }
        public virtual DbSet<PersonalGoalEntity> PersonalGoals { get; set; }
        public virtual DbSet<ChallengeEntity> Challenges {get; set;}
        public virtual DbSet<UserChallengeBridgeEntity> UserChallengeBridge {get; set;}
        public virtual DbSet<ActivityRecordEntity> ActivityRecords {get;set;}
        public virtual DbSet<AchievementEntity> Achievements {get; set;}
        public virtual DbSet<UserAchievementBridgeEntity> UserAchievementBridge {get; set;}
        public virtual DbSet<MindfulnessEntity> Mindfulness { get; set; }
        public virtual DbSet<ChatroomEntity> Chatrooms {get;set;}
        public virtual DbSet<ChatMessageBridgeEntity> ChatMessageBridge {get;set;}
        public virtual DbSet<MessageEntity> Messages {get;set;}
        public virtual DbSet<MessageReactionBridgeEntity> MessageReactionBridge {get;set;}
        public virtual DbSet<ReactionEntity> Reactions {get;set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!_overrideConnectionString) return;

            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserGroupBridgeEntity>()
                .HasKey(ug => ug.UserGroupId);

            modelBuilder.Entity<UserGroupBridgeEntity>()
                .HasOne(ug => ug.UserEntity)
                .WithMany(u => u.UserGroupBridgeEntities)
                .HasForeignKey(ug => ug.UserId);

            modelBuilder.Entity<UserGroupBridgeEntity>()
                .HasOne(ug => ug.GroupEntity)
                .WithMany(g => g.UserGroupBridgeEntities)
                .HasForeignKey(ug => ug.GroupId);

            modelBuilder.Entity<UserChallengeBridgeEntity>()
                .HasKey(ug => ug.UserChallengeId);

            modelBuilder.Entity<UserChallengeBridgeEntity>()
                .HasOne(ug => ug.UserEntity)
                .WithMany(u => u.UserChallengeBridgeEntities)
                .HasForeignKey(ug => ug.UserId);

            modelBuilder.Entity<UserChallengeBridgeEntity>()
                .HasOne(ug => ug.ChallengeEntity)
                .WithMany(g => g.UserChallengeBridgeEntities)
                .HasForeignKey(ug => ug.ChallengeId);   

            modelBuilder.Entity<ChatMessageBridgeEntity>()
                .HasKey(ug => ug.ChatMessageId);

            modelBuilder.Entity<ChatMessageBridgeEntity>()
                .HasOne(cm => cm.ChatroomEntity)
                .WithMany(c => c.ChatMessageBridges)
                .HasForeignKey(cm => cm.ChatId);

            modelBuilder.Entity<ChatMessageBridgeEntity>()
                .HasOne(cm => cm.MessageEntity)
                .WithMany(m => m.ChatMessageBridges)
                .HasForeignKey(cm => cm.MessageId);

            modelBuilder.Entity<MessageReactionBridgeEntity>()
                .HasKey(rm => rm.MessageReactionId);

            modelBuilder.Entity<MessageReactionBridgeEntity>()
                .HasOne(rm => rm.MessageEntity)
                .WithMany(m => m.messageReactionBridges)
                .HasForeignKey(rm => rm.MessageId);

            modelBuilder.Entity<MessageReactionBridgeEntity>()
                .HasOne(rm => rm.ReactionEntity)
                .WithOne(m => m.MessageReactionBridgeEntity)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
