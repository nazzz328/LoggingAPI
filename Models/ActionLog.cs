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
        [Column(TypeName = "jsonb")]
        public string Prev_Value { get; set; }
        [Column(TypeName = "jsonb")]
        public string New_Value { get; set; }
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
}
