using Microsoft.EntityFrameworkCore;
using Task01.Data;
using Task01.Models;
using Task01.Repositories.Interfaces;

namespace Task01.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly ItiDbContext _context;

    public DepartmentRepository(ItiDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Department>> GetAllAsync()
    {
        return await _context.Departments
            .Include(d => d.Students)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Department?> GetByIdAsync(int id)
    {
        return await _context.Departments
            .Include(d => d.Students)
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task AddAsync(Department department)
    {
        _context.Departments.Add(department);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Department department)
    {
        _context.Departments.Update(department);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Department department)
    {
        _context.Departments.Remove(department);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> HasStudentsAsync(int departmentId)
    {
        return await _context.Students.AnyAsync(s => s.DepartmentId == departmentId);
    }
}

