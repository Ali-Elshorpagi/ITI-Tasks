using Lab04.Models;

namespace Lab04.Repositories
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(string id);
    }
}
