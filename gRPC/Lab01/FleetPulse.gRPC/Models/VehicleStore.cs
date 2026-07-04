using ITI.FleetPulse.VehicleService.Protos;

namespace FleetPulse.gRPC.Models
{
    public class VehicleStore
    {
        public Dictionary<string, Vehicle> Vehicles { get; } = new();
    }
}
