using Dapper;
using LoggingAPI.Controllers;
using LoggingAPI.Models;
using Npgsql;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace LoggingAPI.Dapper_Repositories
{
    public class UserRepository
    {
        private string connectionString;

        public UserRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");
        }

        internal System.Data.IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(connectionString);
            }
        }

        public void Add(User item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO users (username, passwordsalt, passwordhash) VALUES (@Username, @PasswordSalt, @PasswordHash)", item);
            }
        }


        public IEnumerable<User> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<User>("SELECT * FROM users");
            }
        }

        public User FindByUsername(UserDto item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<User>("SELECT * FROM users WHERE username = @Username", item).FirstOrDefault();
            }
        }
    }
}
