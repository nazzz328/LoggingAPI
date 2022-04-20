using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LoggingAPI.Models
{
    public class ActionLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; private set; }
        [BsonElement("user_id")]
        public string? User_Id { get; set; }
        [BsonElement("action")]
        public ActionType Action { get; set; }
        [BsonElement("prev_value")]
        public object Prev_Value { get; set; }
        [BsonElement("new_value")]
        public object New_Value { get; set; }
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
