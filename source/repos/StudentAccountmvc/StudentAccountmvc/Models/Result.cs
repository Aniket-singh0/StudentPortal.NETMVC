namespace StudentAccountmvc.Models
{
    public class Result
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public string Subject { get; set; }

        public int Marks { get; set; }

        public int MaxMarks { get; set; }

        public string Grade { get; set; }

        public Student Student { get; set; }
    }
}