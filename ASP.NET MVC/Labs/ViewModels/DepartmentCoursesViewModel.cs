using Task.Models;

namespace Task.ViewModels
{
    public class DepartmentCoursesViewModel
    {
        public Department Department { get; set; }
        public ICollection<Course> AssignedCourses { get; set; } = new List<Course>();
        public ICollection<Course> AvailableCourses { get; set; } = new List<Course>();
    }
}
