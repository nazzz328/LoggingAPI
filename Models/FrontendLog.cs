using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoggingAPI.Models
{
    public class FrontendLog
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string User_Id { get; set; }
        [Column(TypeName = "varchar(300)")]
        public string Url { get; set; }
    }
}
