namespace FleetPulse.API.Dtos;

public class CreateOrderItemDto
{
    public string ProductName { get; set; } = string.Empty;

    public long Quantity { get; set; }

    public double Price { get; set; }

    public double WeightKg { get; set; }
}