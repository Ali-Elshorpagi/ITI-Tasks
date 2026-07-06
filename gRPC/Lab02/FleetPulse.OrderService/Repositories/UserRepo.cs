using FleetPulse.API.Repositories;
using FleetPulse.OrderService.Data;
using FleetPulse.OrderService.Entities;
using Microsoft.EntityFrameworkCore;

namespace FleetPulse.API.Repos
{
    public class UserRepo(FleetDbContext _context) : IUserRepo
    {

        public async Task<bool> UserExistsAsync(string username)
        {
            return await _context.Users
                .AnyAsync(x => x.Username == username);
        }

        public async Task<UserEntity> RegisterAsync(UserEntity user)
        {
            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<UserEntity?> LoginAsync(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Username == username);
        }
    }
}
