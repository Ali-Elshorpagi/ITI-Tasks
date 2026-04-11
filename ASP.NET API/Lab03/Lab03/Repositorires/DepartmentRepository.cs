using Lab03.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab03.Repositorires
{
    public class DepartmentRepository : GenericRepository<Department>
    {
        public DepartmentRepository(ITIContext iTI) : base(iTI) { }
        public List<Department> IncludeStudentsWithDepartments() => context.Departments.Include(d => d.Students).AsNoTracking().ToList();
        public Department? GetDepartmentWithStudents(int deptId)
        {
            return context.Departments
                .Include(d => d.Students)
                .FirstOrDefault(d => d.DeptId == deptId);
        }
    }
}
