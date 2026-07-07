using Microsoft.EntityFrameworkCore;
using Task01.Data;
using Task01.Models;
using Task01.Services.Abstracts;

namespace Task01.Services
{
    public class StudentService(ApplicationDbContext context) : IStudentService
    {
        public async Task<List<Student>> GetAllStudentsAsync()
            => await context.Students.AsNoTracking().ToListAsync();

        public async Task<Student?> GetStudentByIdAsync(int? id)
            => await context.Students.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<string> AddStudentAsync(Student student)
        {
            var exists = await context.Students.AsNoTracking()
                .AnyAsync(s => s.DepartmentId == student.DepartmentId && s.Name == student.Name);

            if (exists)
                return "Failed to Add Student";

            context.Students.Add(student);
            await context.SaveChangesAsync();
            return "Student Added Successfully";
        }
        public async Task<bool> UpdateStudentAsync(Student student)
        {
            var duplicate = await context.Students.AsNoTracking()
                .AnyAsync(x => x.Name!.ToLower() == student.Name!.ToLower()
                            && x.DepartmentId == student.DepartmentId
                            && x.Id != student.Id);

            if (duplicate)
                return false;

            context.Students.Update(student);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task DeleteStudentAsync(int id)
        {
            var student = await context.Students.FindAsync(id);
            if (student is null) return;
            context.Students.Remove(student);
            await context.SaveChangesAsync();
        }
    }
}
