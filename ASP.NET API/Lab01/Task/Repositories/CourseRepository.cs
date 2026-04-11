using Microsoft.EntityFrameworkCore;
using Task.Contexts;
using Task.Models;
using Task.Repositories.IRepositories;

namespace Task.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ITIContext _context;
        public CourseRepository(ITIContext context) =>  _context = context;
        public async Task<Course> AddAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
            return course;
        }
        public async Task<bool> DeleteCourseByIdAsync(string id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course is null)
                return false;

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<ICollection<Course?>> GetAllAsync() => await _context.Courses.ToListAsync();
        public async Task<Course?> GetByIdAsync(string id) => await _context.Courses.FindAsync(id);
        public Task<Course?> GetCourseByNameAsync(string name) => _context.Courses.FirstOrDefaultAsync(c => c.Name == name);
        public async Task<bool> UpdateAsync(string id, Course course)
        {
            var existingCourse = await _context.Courses.FindAsync(id);
            if (existingCourse is null)
                return false;

            existingCourse.Name = course.Name;
            existingCourse.Description = course.Description;
            existingCourse.Duration = course.Duration;

            _context.Courses.Update(existingCourse);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
