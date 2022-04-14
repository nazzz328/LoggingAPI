using Dapper;
using LoggingAPI.Interfaces;
using LoggingAPI.Models;
using Npgsql;
using System.Data;

namespace LoggingAPI.Dapper_Repositories
{
    public class BackendLogRepository : IRepository <BackendLog>
    {
        private string connectionString;
        public BackendLogRepository(IConfiguration configuration)
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
        public void Add(BackendLog item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO backendlogs (message, date, user_id, source) VALUES (@Message, @Date, @User_Id, @Source)", item);
            }
        }

        public IEnumerable<BackendLog> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<BackendLog>("SELECT * FROM backendlogs");
            }
        }
    }
}
