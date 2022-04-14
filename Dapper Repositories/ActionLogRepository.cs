using Dapper;
using LoggingAPI.Interfaces;
using LoggingAPI.Models;
using Npgsql;
using System.Data;

namespace LoggingAPI.Dapper_Repositories
{
    public class ActionLogRepository : IRepository<ActionLog>
    {
        private string connectionString;
        public ActionLogRepository(IConfiguration configuration)
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
        public void Add(ActionLog item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO actionlogs (user_id, action, prev_value, new_value, unit_type, date, source) VALUES (@User_Id, @Action, @Prev_Value, @New_Value, @Unit_Type, @Date, @Source)", item);
            }
        }

        public IEnumerable<ActionLog> FindAll()
        {
                using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<ActionLog>("SELECT * FROM actionlogs");
            }
        }
    }
}
