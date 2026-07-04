using FleetPulse.gRPC.Models;
using Grpc.Core;
using ITI.FleetPulse.OrderService.Protos;

namespace FleetPulse.gRPC.Services
{
    public class FleetOperationsService : ITI.FleetPulse.OrderService.Protos.FleetOperationsService.FleetOperationsServiceBase
    {
        private readonly OrderStore _store;
        public FleetOperationsService(OrderStore store) => _store = store;
        public override Task<OrderReply> CreateOrder(CreateOrderRequest request, ServerCallContext context)
        {
            var order = new Order
            {
                OrderId = Guid.NewGuid().ToString(),

                CustomerName = request.CustomerName,

                Status = DeliveryStatus.Pending,

                RequestedAt = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow),

                EstimatedDeliveryTime = Google.Protobuf.WellKnownTypes.Duration.FromTimeSpan(TimeSpan.FromHours(2))
            };

            order.Items.AddRange(request.Items);

            order.ExtraInfo.Add(request.ExtraInfo);

            if (request.DeliveryNotes is not null)
                order.DeliveryNotes = request.DeliveryNotes;

            switch (request.PackageDetailsCase)
            {
                case CreateOrderRequest.PackageDetailsOneofCase.FragilePackage:
                    order.FragilePackage = request.FragilePackage;
                    break;

                case CreateOrderRequest.PackageDetailsOneofCase.ColdPackage:
                    order.ColdPackage = request.ColdPackage;
                    break;

                case CreateOrderRequest.PackageDetailsOneofCase.StandardPackage:
                    order.StandardPackage = request.StandardPackage;
                    break;
            }

            _store.Orders.Add(order.OrderId, order);

            return Task.FromResult(new OrderReply { Order = order });
        }
        public override Task<OrderReply> GetOrder(GetOrderRequest request, ServerCallContext context)
        {
            if (!_store.Orders.TryGetValue(request.OrderId, out var order))
                throw new RpcException(new Status(StatusCode.NotFound, "Order not found"));

            return Task.FromResult(new OrderReply { Order = order });
        }
        public override Task<OrderReply> UpdateOrderStatus(UpdateOrderStatusRequest request, ServerCallContext context)
        {
            if (!_store.Orders.TryGetValue(request.OrderId, out var order))
                throw new RpcException(new Status(StatusCode.NotFound, "Order not found"));

            order.Status = request.NewStatus;

            return Task.FromResult(new OrderReply { Order = order });
        }
        public override Task<ListOrdersReply> ListOrders(ListOrdersRequest request, ServerCallContext context)
        {
            var reply = new ListOrdersReply();

            if (request.StatusFilter == DeliveryStatus.Unspecified)
                reply.Orders.AddRange(_store.Orders.Values);
            else
                reply.Orders.AddRange(_store.Orders.Values.Where(o => o.Status == request.StatusFilter));

            return Task.FromResult(reply);
        }

    }
}
