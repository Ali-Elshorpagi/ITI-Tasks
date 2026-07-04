using Grpc.Net.Client;
using ITI.FleetPulse.OrderService.Protos;

namespace FleetPulse.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            using var channel = GrpcChannel.ForAddress("https://localhost:7172", new GrpcChannelOptions { HttpHandler = handler });

            var client = new FleetOperationsService.FleetOperationsServiceClient(channel);

            var request = new CreateOrderRequest
            {
                CustomerName = "Ahmed"
            };

            request.Items.Add(new OrderItem
            {
                ProductName = "Laptop",
                Quantity = 1,
                Price = 25000,
                WeightKg = 2.5
            });

            request.ExtraInfo.Add("City", "Cairo");

            request.DeliveryNotes = "Call before delivery";

            request.StandardPackage = new StandardPackage();

            var reply = await client.CreateOrderAsync(request);

            Console.WriteLine("\n--------------Order Creation-------------\n");

            Console.WriteLine($"Order Id: {reply.Order.OrderId}");
            Console.WriteLine($"Customer: {reply.Order.CustomerName}");
            Console.WriteLine($"Status: {reply.Order.Status}");
            string orderId = reply.Order.OrderId;


            Console.WriteLine("\n--------------Get Order-------------\n");

            var getRequest = new GetOrderRequest
            {
                OrderId = orderId
            };

            var getReply = await client.GetOrderAsync(getRequest);

            Console.WriteLine($"Customer : {getReply.Order.CustomerName}");
            Console.WriteLine($"Status   : {getReply.Order.Status}");
            Console.WriteLine($"Items    : {getReply.Order.Items.Count}");


            Console.WriteLine("\n--------------Update Order-------------\n");

            var updateRequest = new UpdateOrderStatusRequest
            {
                OrderId = orderId,
                NewStatus = DeliveryStatus.Delivered
            };

            var updateReply = await client.UpdateOrderStatusAsync(updateRequest);

            Console.WriteLine($"New Status : {updateReply.Order.Status}");


            Console.WriteLine("\n--------------List All Orders-------------\n");

            var listReply = await client.ListOrdersAsync(new ListOrdersRequest());

            foreach (var order in listReply.Orders)
            {
                Console.WriteLine(
                    $"{order.OrderId} | {order.CustomerName} | {order.Status}");
            }



            Console.WriteLine("\n--------------Filtered Orders-------------\n");

            var deliveredOrders = await client.ListOrdersAsync(new ListOrdersRequest
            {
                StatusFilter = DeliveryStatus.Delivered
            });

            foreach (var order in deliveredOrders.Orders)
            {
                Console.WriteLine(order.OrderId);
            }
        }
    }
}
