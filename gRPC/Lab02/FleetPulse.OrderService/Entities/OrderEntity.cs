namespace FleetPulse.OrderService.Entities;

public class OrderEntity
{
    public Guid Id { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public int Status { get; set; }

    public DateTime RequestedAt { get; set; }

    public TimeSpan EstimatedDeliveryTime { get; set; }

    public string? DeliveryNotes { get; set; }

    public ICollection<OrderItemEntity> Items { get; set; }
        = new List<OrderItemEntity>();
}