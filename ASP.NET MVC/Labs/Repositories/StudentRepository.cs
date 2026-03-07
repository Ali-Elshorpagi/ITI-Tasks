using Microsoft.EntityFrameworkCore;
using Task.Context;
using Task.Models;

namespace Task.Repositories
{
    public class StudentRepository : Repository<Student>
    {
        public StudentRepository(AppDbContext context) : base(context) { }
        public override List<Student> GetAll() => _dbSet.Include(s => s.Department).ToList();
        public override Student GetById(int id) => _dbSet.Include(s => s.Department).FirstOrDefault(s => s.Id == id) ?? new Student();
    }
}
