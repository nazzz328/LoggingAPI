using Dapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace LoggingAPI.Models
{
    public class ActionLog
    {
        public int Id { get; set; }
        public string User_Id { get; set; }
        public ActionType Action { get; set; }
        public Json Prev_Value { get; set; }
        public Json New_Value { get; set; }
        public string Unit_Type { get; set; }
        public DateTime Date { get; set; }
        public SourceType Source { get; set; }

        public enum ActionType
        {
            CREATE,
            UPDATE,
            REMOVE
        }

        public enum SourceType
        {
            WEBAPP,
            ANDROIDAPP,
            IOSAPP,
            UNKNOWN
        }

    }

    public class Json
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
