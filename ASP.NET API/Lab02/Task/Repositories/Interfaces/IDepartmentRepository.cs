using Task01.Models;

namespace Task01.Repositories.Interfaces;

public interface IDepartmentRepository
{
    Task<ICollection<Department>> GetAllAsync();
    Task<Department?> GetByIdAsync(int id);
    Task AddAsync(Department department);
    Task UpdateAsync(Department department);
    Task DeleteAsync(Department department);
    Task<bool> HasStudentsAsync(int departmentId);
}
