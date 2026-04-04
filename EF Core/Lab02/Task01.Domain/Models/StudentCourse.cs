namespace Task01.Domain.Models
{
    public class StudentCourse
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int Degree { get; set; }
        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
    }
}
