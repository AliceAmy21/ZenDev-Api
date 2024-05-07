﻿using Microsoft.EntityFrameworkCore;
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
            _connectionString = "Data Source=127.0.0.1\\ZenDev_DB,1433;database=ZenDev.Dev;User ID=SA;Password=ThreeAWithTAnd1J;TrustServerCertificate=true;";
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!_overrideConnectionString) return;

            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
