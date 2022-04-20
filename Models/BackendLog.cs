using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoggingAPI.Models
{
    public class BackendLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; private set; }
        [BsonElement("message")]
        public string? Message { get; set; }
        [BsonElement("date")]
        public DateTime Date { get; set; }
        [BsonElement("user_id")]
        public string? User_Id { get; set; }
        [BsonElement("source")]
        public string? Source { get; set; }
    }
}
