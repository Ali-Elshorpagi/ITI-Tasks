using Task01.Models;

namespace Task01.Services.Abstracts
{
    public interface IDepartmentService
    {
        Task<List<Department>> GetAllDepartmentsAsync();
        Task<Department?> GetDepartmentByIdAsync(int? id);
        Task<bool> AddDepartmentAsync(Department department);
        Task<bool> EditDepartmentAsync(Department department);
        Task DeleteDepartmentAsync(int id);
    }
}
