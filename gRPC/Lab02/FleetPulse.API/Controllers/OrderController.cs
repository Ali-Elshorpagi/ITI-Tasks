using FleetPulse.API.Dtos;
using FleetPulse.OrderService.Protos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FleetPulse.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly FleetOperationsService.FleetOperationsServiceClient _client;

    public OrdersController(FleetOperationsService.FleetOperationsServiceClient client)
    {
        _client = client;
    }


    [Authorize(Roles = "Admin")]
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var reply = await _client.GetOrderAsync(
            new GetOrderRequest
            {
                OrderId = id
            });

        return Ok(reply);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderDto dto)
    {
        var request = new CreateOrderRequest
        {
            CustomerName = dto.CustomerName
        };

        foreach (var item in dto.Items)
        {
            request.Items.Add(new OrderItem
            {
                ProductName = item.ProductName,
                Quantity = item.Quantity,
                Price = item.Price,
                WeightKg = item.WeightKg
            });
        }

        if (!string.IsNullOrWhiteSpace(dto.DeliveryNotes))
        {
            request.DeliveryNotes = dto.DeliveryNotes;
        }

        request.StandardPackage = new StandardPackage();

        var reply = await _client.CreateOrderAsync(request);

        return Ok(reply);
    }

}
