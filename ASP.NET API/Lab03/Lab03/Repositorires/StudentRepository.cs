using Lab03.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab03.Repositorires
{
    public class StudentRepository : GenericRepository<Student>
    {
        public StudentRepository(ITIContext c) : base(c) { }
        public IQueryable<Student> StudentsWithSuperAndDept() => context.Students.Include(s => s.Dept).Include(s => s.StSuperNavigation).AsQueryable();
    }
}
