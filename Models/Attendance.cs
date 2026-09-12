using System.ComponentModel.DataAnnotations.Schema;

namespace StudentAccountmvc.Models
{
    [Table("Attendance")]
    public class Attendance
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public string Subject { get; set; }

        public int? TotalClasses { get; set; }

        public int? PresentClasses { get; set; }
    }
}