using FleetPulse.OrderService.Entities;

namespace FleetPulse.API.Repositories
{
    public interface IUserRepo
    {
        Task<bool> UserExistsAsync(string username);

        Task<UserEntity> RegisterAsync(UserEntity user);

        Task<UserEntity?> LoginAsync(string username);
    }
}
