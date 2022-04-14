using Dapper;
using LoggingAPI.Interfaces;
using LoggingAPI.Models;
using Npgsql;
using System.Data;

namespace LoggingAPI.Dapper_Repositories
{
    public class FrontendLogRepository : IRepository<FrontendLog>
    {
        private string connectionString;
        public FrontendLogRepository(IConfiguration configuration)
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
        public void Add(FrontendLog item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO frontendlogs (message, date, user_id, url) VALUES (@Message, @Date, @User_Id, @Url)", item);
            }
        }

        public IEnumerable<FrontendLog> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<FrontendLog>("SELECT * FROM frontendlogs");
            }
        }
    }
}
