using System.ComponentModel.DataAnnotations;

namespace StudentAccountmvc.Models
{
    public class Student
    {
        internal readonly object ConfirmPassword;

        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Mobile { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Gender { get; set; }

        public string Course { get; set; }

        public string Branch { get; set; }

        public string CollegeName { get; set; }

        public string EnrollmentNumber { get; set; }

        [Required]
        public string Password { get; set; }
    }
}