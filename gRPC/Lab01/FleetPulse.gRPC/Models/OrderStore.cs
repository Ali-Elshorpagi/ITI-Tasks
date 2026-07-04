using ITI.FleetPulse.OrderService.Protos;

namespace FleetPulse.gRPC.Models
{
    public class OrderStore
    {
        public Dictionary<string, Order> Orders { get; } = new();
    }
}
