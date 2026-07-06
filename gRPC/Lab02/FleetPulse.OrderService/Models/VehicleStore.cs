using FleetPulse.OrderService.Protos;

namespace FleetPulse.OrderService.Advanced.Models
{
    public class VehicleStore
    {
        public Dictionary<string, Vehicle> Vehicles { get; } = new();
    }
}
