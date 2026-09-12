namespace StudentAccountmvc.Models
{
    public class Assignment
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public string Title { get; set; }

        public string Subject { get; set; }

        public DateTime? DueDate { get; set; }

        public string Status { get; set; }

        public Student Student { get; set; }
    }
}