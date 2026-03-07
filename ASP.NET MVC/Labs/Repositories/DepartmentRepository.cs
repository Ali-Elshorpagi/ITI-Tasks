using Microsoft.EntityFrameworkCore;
using Task.Context;
using Task.Models;

namespace Task.Repositories
{
    public class DepartmentRepository : Repository<Department>
    {
        public DepartmentRepository(AppDbContext context) : base(context) { }
        public override Department? GetById(int id) => _context.Departments.Include(d => d.Courses).FirstOrDefault(d => d.Id == id);
    }
}
