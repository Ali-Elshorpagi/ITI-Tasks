namespace FleetPulse.API.Dtos;

public class CreateOrderDto
{
    public string CustomerName { get; set; } = string.Empty;

    public List<CreateOrderItemDto> Items { get; set; } = new();

    public string? DeliveryNotes { get; set; }
}