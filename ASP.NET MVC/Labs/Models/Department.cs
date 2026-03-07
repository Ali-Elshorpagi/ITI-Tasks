namespace Task.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public virtual ICollection<Student> Students { get; set; } = new List<Student>();
        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
