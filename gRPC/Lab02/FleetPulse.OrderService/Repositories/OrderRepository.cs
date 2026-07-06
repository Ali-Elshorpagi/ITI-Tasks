using FleetPulse.OrderService.Data;
using FleetPulse.OrderService.Entities;
using Microsoft.EntityFrameworkCore;

namespace FleetPulse.OrderService.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly FleetDbContext _context;

    public OrderRepository(FleetDbContext context)
    {
        _context = context;
    }

    public async Task<OrderEntity> AddAsync(OrderEntity order)
    {
        _context.Orders.Add(order);

        await _context.SaveChangesAsync();

        return order;
    }

    public async Task<OrderEntity?> GetByIdAsync(Guid id)
    {
        return await _context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<List<OrderEntity>> GetAllAsync()
    {
        return await _context.Orders
            .Include(o => o.Items)
            .ToListAsync();
    }

    public async Task<OrderEntity?> UpdateStatusAsync(Guid id, int status)
    {
        var order = await _context.Orders.FindAsync(id);

        if (order == null)
            return null;

        order.Status = status;

        await _context.SaveChangesAsync();

        return order;
    }
}