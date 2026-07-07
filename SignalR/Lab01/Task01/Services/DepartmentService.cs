using Microsoft.EntityFrameworkCore;
using Task01.Data;
using Task01.Models;
using Task01.Services.Abstracts;

namespace Task01.Services
{
    public class DepartmentService(ApplicationDbContext context) : IDepartmentService
    {
        public async Task<List<Department>> GetAllDepartmentsAsync()
            => await context.Departments.AsNoTracking().ToListAsync();

        public async Task<Department?> GetDepartmentByIdAsync(int? id)
            => await context.Departments.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<bool> AddDepartmentAsync(Department department)
        {
            var exists = await context.Departments.AsNoTracking()
                .AnyAsync(x => x.Name!.ToLower() == department.Name!.ToLower());

            if (exists)
                return false;

            context.Departments.Add(department);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> EditDepartmentAsync(Department department)
        {
            var existing = await context.Departments.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == department.Id);

            if (existing is null)
                return false;

            if (existing.Name?.ToLower() == department.Name?.ToLower()
                && existing.Description?.ToLower() == department.Description?.ToLower())
                return false;

            context.Departments.Update(department);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task DeleteDepartmentAsync(int id)
        {
            var department = await context.Departments.FindAsync(id);
            if (department is null) return;
            context.Departments.Remove(department);
            await context.SaveChangesAsync();
        }
    }
}
