using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace StudentAccountmvc.Models
{
    [Table("Courses")] // Database ki exact table mapping
    public class Course
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public string CourseName { get; set; }

        public string Instructor { get; set; }

        public string Duration { get; set; }

        // Navigation property ko EF save operation se detach karein
        [ForeignKey("StudentId")]
        [ValidateNever]
        public virtual Student? Student { get; set; }
    }
}