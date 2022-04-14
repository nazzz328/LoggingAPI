using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoggingAPI.Models
{
    public class BackendLog
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public string User_Id { get; set; }
        public string Source { get; set; }
    }
}
