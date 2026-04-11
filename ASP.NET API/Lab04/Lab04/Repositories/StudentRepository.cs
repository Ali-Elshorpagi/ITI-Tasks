using Lab04.DTOs;
using Lab04.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Lab04.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly UserManager<Student> _userManager;
        public StudentRepository(UserManager<Student> userManager)
        {
            _userManager = userManager;
        }
        public async Task<List<Student>> GetAllAsync() => await _userManager.Users.ToListAsync();
        public async Task<Student?> GetByIdAsync(string id) => await _userManager.FindByIdAsync(id);
    }
}
