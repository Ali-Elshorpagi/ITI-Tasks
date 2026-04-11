using Lab04.DTOs;
using Microsoft.AspNetCore.Identity;

namespace Lab04.Repositories
{
    public interface IAccounttRepository
    {
        Task<IdentityResult> RegisterAsync(AddStudentDTO dto);
        Task<string?> LoginAsync(LoginDataDTo dto);
    }
}
