using Microsoft.EntityFrameworkCore;
using Task01.Data;
using Task01.Models;
using Task01.Repositories.Interfaces;

namespace Task01.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly ItiDbContext _context;

    public StudentRepository(ItiDbContext context)
    {
        _context = context;
    }

    public async Task<(ICollection<Student> Students, int TotalCount)> GetPagedAsync(string? search, int pageNumber, int pageSize)
    {
        var query = _context.Students
            .Include(s => s.Department)
            .Include(s => s.Supervisor)
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var searchTerm = search.Trim().ToLower();
            query = query.Where(s =>
                s.FirstName.ToLower().Contains(searchTerm) ||
                (s.LastName != null && s.LastName.ToLower().Contains(searchTerm)) ||
                (s.Address != null && s.Address.ToLower().Contains(searchTerm)) ||
                (s.Department != null && s.Department.Name.ToLower().Contains(searchTerm)));
        }

        var totalCount = await query.CountAsync();

        var students = await query
            .OrderBy(s => s.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (students, totalCount);
    }

    public async Task<Student?> GetByIdAsync(int id)
    {
        return await _context.Students
            .Include(s => s.Department)
            .Include(s => s.Supervisor)
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Students.AnyAsync(s => s.Id == id);
    }

    public async Task<bool> DepartmentExistsAsync(int departmentId)
    {
        return await _context.Departments.AnyAsync(d => d.Id == departmentId);
    }

    public async Task AddAsync(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Student student)
    {
        _context.Students.Update(student);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Student student)
    {
        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
    }
}

