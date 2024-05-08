
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Point.API.Data.Interfaces;
using Point.API.Domain.Services;
using System.Data.Common;

namespace Point.API.Data.Context
{
    public class PointContext : DbContext, IDbContext
    {
        #region Ctor
#pragma warning disable CS8618
        public PointContext(DbContextOptions<PointContext> options) : base(options)
        {
        }
        #endregion

        #region Methods
        public DbConnection GetConnection()
        {
            return Database.GetDbConnection();
        }
        #endregion

        #region DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<UserPoint> Points{ get; set; }
        #endregion

        #region Override
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("AspNetUsers", "Identity");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .Build();

            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=PointSysDb;", x => x.MigrationsHistoryTable("__MigrationHistory", "Identity"));
        }
        #endregion
    }
}
