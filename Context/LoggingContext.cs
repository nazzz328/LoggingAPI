using LoggingAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Security.Cryptography;
using System.Text;

namespace LoggingAPI.Context
{
    public class LoggingContext : DbContext
    {
        /*
         * 
         *          LoggingContext class is preserved for possible transition to EF Core in the future
         * 
         */

        public LoggingContext(DbContextOptions<LoggingContext> options) : 
            base(options) 
            {

            }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<ActionLog>().Property(p => p.Action).HasConversion<string>();
            //modelBuilder.Entity<ActionLog>().Property(p => p.Source).HasConversion<string>();

            //User admin = new User {Id = 1, Username = "admin", Password = "password"};
            //CreatePasswordHash (admin.Password, out byte[] passwordHash, out byte[] passwordSalt);
            //admin.PasswordHash = passwordHash;
            //admin.PasswordSalt = passwordSalt;

            //modelBuilder.Entity<User>().HasData(new User [] {admin});
        }


        public DbSet<ActionLog> ActionLogs { get; set; }
        public DbSet<BackendLog> BackendLogs { get; set; }
        public DbSet<FrontendLog> FrontendLogs { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
