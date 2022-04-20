using Dapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LoggingAPI.Models
{
    public class ActionLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("user_id")]
        public string? User_Id { get; set; }
        [BsonElement("action")]
        public ActionType Action { get; set; }
        [BsonElement("prev_value")]
        public string? Prev_Value { get; set; }
        [BsonElement("new_value")]
        public string? New_Value { get; set; }
        [BsonElement("unit_type")]
        public string? Unit_Type { get; set; }
        [BsonElement("date")]
        public DateTime Date { get; set; }
        [BsonElement("source")]
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

    }


}
