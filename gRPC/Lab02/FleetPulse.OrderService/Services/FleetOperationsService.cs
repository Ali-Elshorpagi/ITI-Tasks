using Grpc.Core;
using FleetPulse.OrderService.Entities;
using FleetPulse.OrderService.Protos;
using FleetPulse.OrderService.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace FleetPulse.OrderService.Advanced.Services
{
    [Authorize]
    public class FleetOperationsService : FleetPulse.OrderService.Protos.FleetOperationsService.FleetOperationsServiceBase
    {
        private readonly IOrderRepository _repository;
        public FleetOperationsService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public override async Task<OrderReply> CreateOrder(CreateOrderRequest request, ServerCallContext context)
        {
            var entity = new OrderEntity
            {
                Id = Guid.NewGuid(),
                CustomerName = request.CustomerName,
                Status = (int)DeliveryStatus.Pending,
                RequestedAt = DateTime.UtcNow,
                EstimatedDeliveryTime = TimeSpan.FromHours(2),
                DeliveryNotes = request.DeliveryNotes
            };

            foreach (var item in request.Items)
            {
                entity.Items.Add(new OrderItemEntity
                {
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    WeightKg = item.WeightKg
                });
            }

            await _repository.AddAsync(entity);

            var order = new Order
            {
                OrderId = entity.Id.ToString(),
                CustomerName = entity.CustomerName,
                Status = (DeliveryStatus)entity.Status,
                RequestedAt = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(
                    DateTime.SpecifyKind(entity.RequestedAt, DateTimeKind.Utc)),
                EstimatedDeliveryTime =
                    Google.Protobuf.WellKnownTypes.Duration.FromTimeSpan(entity.EstimatedDeliveryTime)
            };

            order.Items.AddRange(entity.Items.Select(i => new OrderItem
            {
                ProductName = i.ProductName,
                Quantity = i.Quantity,
                Price = i.Price,
                WeightKg = i.WeightKg
            }));

            if (!string.IsNullOrWhiteSpace(entity.DeliveryNotes))
                order.DeliveryNotes = entity.DeliveryNotes;

            return new OrderReply
            {
                Order = order
            };
        }
        public override async Task<OrderReply> GetOrder(GetOrderRequest request, ServerCallContext context)
        {
            var entity = await _repository.GetByIdAsync(Guid.Parse(request.OrderId));

            if (entity == null)
            {
                throw new RpcException(
                    new Status(StatusCode.NotFound, "Order not found"));
            }

            var order = new Order
            {
                OrderId = entity.Id.ToString(),
                CustomerName = entity.CustomerName,
                Status = (DeliveryStatus)entity.Status,
                RequestedAt = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(
                    DateTime.SpecifyKind(entity.RequestedAt, DateTimeKind.Utc)),
                EstimatedDeliveryTime =
                    Google.Protobuf.WellKnownTypes.Duration.FromTimeSpan(entity.EstimatedDeliveryTime)
            };

            order.Items.AddRange(entity.Items.Select(i => new OrderItem
            {
                ProductName = i.ProductName,
                Quantity = i.Quantity,
                Price = i.Price,
                WeightKg = i.WeightKg
            }));

            if (!string.IsNullOrWhiteSpace(entity.DeliveryNotes))
                order.DeliveryNotes = entity.DeliveryNotes;

            return new OrderReply
            {
                Order = order
            };
        }
        public override async Task<OrderReply> UpdateOrderStatus(UpdateOrderStatusRequest request, ServerCallContext context)
        {
            var entity = await _repository.UpdateStatusAsync(
                Guid.Parse(request.OrderId),
                (int)request.NewStatus);

            if (entity == null)
            {
                throw new RpcException(
                    new Status(StatusCode.NotFound, "Order not found"));
            }

            var order = new Order
            {
                OrderId = entity.Id.ToString(),
                CustomerName = entity.CustomerName,
                Status = (DeliveryStatus)entity.Status,
                RequestedAt = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(
                    DateTime.SpecifyKind(entity.RequestedAt, DateTimeKind.Utc)),
                EstimatedDeliveryTime =
                    Google.Protobuf.WellKnownTypes.Duration.FromTimeSpan(entity.EstimatedDeliveryTime)
            };

            return new OrderReply
            {
                Order = order
            };
        }


        public override async Task<ListOrdersReply> ListOrders(ListOrdersRequest request, ServerCallContext context)
        {
            var entities = await _repository.GetAllAsync();

            if (request.StatusFilter != DeliveryStatus.Unspecified)
            {
                entities = entities
                    .Where(o => o.Status == (int)request.StatusFilter)
                    .ToList();
            }

            var reply = new ListOrdersReply();

            foreach (var entity in entities)
            {
                var order = new Order
                {
                    OrderId = entity.Id.ToString(),
                    CustomerName = entity.CustomerName,
                    Status = (DeliveryStatus)entity.Status,
                    RequestedAt = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(
                        DateTime.SpecifyKind(entity.RequestedAt, DateTimeKind.Utc)),
                    EstimatedDeliveryTime =
                        Google.Protobuf.WellKnownTypes.Duration.FromTimeSpan(entity.EstimatedDeliveryTime)
                };

                order.Items.AddRange(entity.Items.Select(i => new OrderItem
                {
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    Price = i.Price,
                    WeightKg = i.WeightKg
                }));

                if (!string.IsNullOrWhiteSpace(entity.DeliveryNotes))
                    order.DeliveryNotes = entity.DeliveryNotes;

                reply.Orders.Add(order);
            }

            return reply;
        }

    }
}
