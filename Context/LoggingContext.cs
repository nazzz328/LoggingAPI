using LoggingAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LoggingAPI.Context
{
    public class LoggingContext : DbContext
    {
        public LoggingContext(DbContextOptions<LoggingContext> options) : 
            base(options) 
            { 

                Database.EnsureCreated();
            }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActionLog>()
                .Property(p => p.Action)
                .HasConversion<string>();

            modelBuilder.Entity<ActionLog>()
                .Property(p => p.Source)
                .HasConversion<string>();

        }

        public DbSet<ActionLog> ActionLogs { get; set; }
        public DbSet<BackendLog> BackendLogs { get; set; }
        public DbSet<FrontendLog> FrontendLogs { get; set; }
    }
}
