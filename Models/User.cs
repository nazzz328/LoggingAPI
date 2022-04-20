using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoggingAPI.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("username")]
        public string Username { get; set; } = string.Empty;
        [BsonElement("passwordhash")]
        public byte[]? PasswordHash { get; set; }
        [BsonElement("passwordsalt")]
        public byte[]? PasswordSalt { get; set; }
    }
}