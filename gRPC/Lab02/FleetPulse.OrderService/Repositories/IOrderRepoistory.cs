using FleetPulse.OrderService.Entities;

namespace FleetPulse.OrderService.Repositories;

public interface IOrderRepository
{
    Task<OrderEntity> AddAsync(OrderEntity order);

    Task<OrderEntity?> GetByIdAsync(Guid id);

    Task<List<OrderEntity>> GetAllAsync();

    Task<OrderEntity?> UpdateStatusAsync(Guid id, int status);
}