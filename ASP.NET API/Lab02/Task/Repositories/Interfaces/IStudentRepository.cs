using Task01.Models;

namespace Task01.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        Task<(ICollection<Student> Students, int TotalCount)> GetPagedAsync(string? search, int pageNumber, int pageSize);
        Task<Student?> GetByIdAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> DepartmentExistsAsync(int departmentId);
        Task AddAsync(Student student);
        Task UpdateAsync(Student student);
        Task DeleteAsync(Student student);
    }
}
