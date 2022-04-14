using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoggingAPI.Models
{
    public class User
    {
        [Key]
        public int Id {get; set; }
        public string Username { get; set; } = string.Empty;
        [NotMapped]
        public byte[] PasswordHash { get; set; } 
        public byte[] PasswordSalt { get; set; }
    }
}