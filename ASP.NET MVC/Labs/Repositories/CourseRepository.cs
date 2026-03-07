using Task.Context;
using Task.Models;

namespace Task.Repositories
{
    public class CourseRepository : Repository<Course>
    {
        public CourseRepository(AppDbContext context) : base(context) { }
    }
}
