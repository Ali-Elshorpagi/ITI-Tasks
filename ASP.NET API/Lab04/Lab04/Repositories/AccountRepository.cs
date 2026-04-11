using Lab04.DTOs;
using Lab04.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Lab04.Repositories
{
    public class AccountRepository : IAccounttRepository
    {
        private readonly UserManager<Student> _userManager;
        private readonly SignInManager<Student> _signInManager;
        private readonly IConfiguration _configuration;
        public AccountRepository(UserManager<Student> userManager, SignInManager<Student> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }
        public async Task<IdentityResult> RegisterAsync(AddStudentDTO dto)
        {
            var student = new Student
            {
                Fullname = dto.Fullname,
                Age = dto.Age,
                UserName = dto.Uusername,
                Email = dto.Email
            };

            var createResult = await _userManager.CreateAsync(student, dto.Password);
            if (!createResult.Succeeded)
                return createResult;

            var roleResult = await _userManager.AddToRoleAsync(student, "teacher");
            return roleResult;
        }
        public async Task<string?> LoginAsync(LoginDataDTo dto)
        {
            var loginResult = await _signInManager.PasswordSignInAsync(dto.Username, dto.Password, false, false);
            if (!loginResult.Succeeded)
                return null;

            var user = await _userManager.FindByNameAsync(dto.Username);
            if (user is null)
                return null;

            var claims = new List<Claim>
            {
                new("name", user.UserName ?? string.Empty),
                new(ClaimTypes.Email, user.Email ?? string.Empty),
                new(ClaimTypes.NameIdentifier, user.Id)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var key = _configuration["Jwt:Key"] ?? string.Empty;
            if (string.IsNullOrWhiteSpace(key))
                return null;

            var secKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
            var signingCredentials = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
