using Task.Models;

namespace Task.Repositories.IRepositories
{
    public interface ICourseRepository
    {
        Task<ICollection<Course?>> GetAllAsync();
        Task<Course?> GetByIdAsync(string id);
        Task<bool> DeleteCourseByIdAsync(string id);
        Task<bool> UpdateAsync(string id, Course course);
        Task<Course> AddAsync(Course course);
        Task<Course?> GetCourseByNameAsync(string name);
    }
}
