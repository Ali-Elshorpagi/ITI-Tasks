using FleetPulse.OrderService.Advanced.Models;
using Grpc.Core;
using FleetPulse.OrderService.Protos;

namespace FleetPulse.OrderService.Advanced.Services
{
    public class VehicleService : FleetPulse.OrderService.Protos.VehicleService.VehicleServiceBase
    {
        private readonly VehicleStore _store;

        public VehicleService(VehicleStore store)
        {
            _store = store;
        }


        public override Task<VehicleReply> RegisterVehicle(RegisterVehicleRequest request, ServerCallContext context)
        {
            var vehicle = new Vehicle
            {
                VehicleId = Guid.NewGuid().ToString(),
                PlateNumber = request.PlateNumber,
                VehicleType = request.VehicleType,
                CapacityKg = request.CapacityKg,
                IsActive = true
            };

            _store.Vehicles.Add(vehicle.VehicleId, vehicle);

            return Task.FromResult(new VehicleReply
            {
                Vehicle = vehicle
            });
        }


        public override Task<VehicleReply> GetVehicle(GetVehicleRequest request, ServerCallContext context)
        {
            if (!_store.Vehicles.TryGetValue(request.VehicleId, out var vehicle))
            {
                throw new RpcException(
                    new Status(StatusCode.NotFound, "Vehicle not found"));
            }

            return Task.FromResult(new VehicleReply
            {
                Vehicle = vehicle
            });
        }

    }
}
