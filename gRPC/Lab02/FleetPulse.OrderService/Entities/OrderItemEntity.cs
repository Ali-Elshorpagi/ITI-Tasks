namespace FleetPulse.OrderService.Entities;

public class OrderItemEntity
{
    public Guid Id { get; set; }

    public Guid OrderId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public long Quantity { get; set; }

    public double Price { get; set; }

    public double WeightKg { get; set; }

    public OrderEntity Order { get; set; } = null!;
}