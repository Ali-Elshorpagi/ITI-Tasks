using Task01.Models;

namespace Task01.Services.Abstracts
{
    public interface IStudentService
    {
        Task<string> AddStudentAsync(Student student);
        Task<List<Student>> GetAllStudentsAsync();
        Task<Student?> GetStudentByIdAsync(int? id);
        Task<bool> UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(int id);
    }
}
