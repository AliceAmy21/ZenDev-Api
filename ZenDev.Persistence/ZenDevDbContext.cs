using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ZenDev.Persistence.Entities;
using System.Xml.Linq;

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
        public virtual DbSet<UserGroupBridgeEntity> UserGroupBridge { get; set; }
        public virtual DbSet<PersonalGoalEntity> PersonalGoals { get; set; }
        public virtual DbSet<ChallengeEntity> Challenges {get; set;}
        public virtual DbSet<UserChallengeBridgeEntity> UserChallengeBridge {get; set;}


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
        }
    }
}
