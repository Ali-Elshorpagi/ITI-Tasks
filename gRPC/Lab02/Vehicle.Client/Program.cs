using Grpc.Net.Client;
using Vehicle.Client.Protos;

namespace Vehicle.Client.Advanced
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7172");

            var vehicleClient = new VehicleService.VehicleServiceClient(channel);

            var registerRequest = new RegisterVehicleRequest
            {
                PlateNumber = "ABC-123",
                VehicleType = VehicleType.Truck,
                CapacityKg = 2500
            };

            var registerReply = await vehicleClient.RegisterVehicleAsync(registerRequest);

            Console.WriteLine("\n-----------Vehicle Registered Successfully------------\n");

            Console.WriteLine($"Id       : {registerReply.Vehicle.VehicleId}");
            Console.WriteLine($"Plate    : {registerReply.Vehicle.PlateNumber}");
            Console.WriteLine($"Type     : {registerReply.Vehicle.VehicleType}");
            Console.WriteLine($"Capacity : {registerReply.Vehicle.CapacityKg}");
            Console.WriteLine($"Active   : {registerReply.Vehicle.IsActive}");

            string vehicleId = registerReply.Vehicle.VehicleId;

            Console.WriteLine("\n-----------Get Vehicle------------\n");

            var getRequest = new GetVehicleRequest
            {
                VehicleId = vehicleId
            };

            var getReply = await vehicleClient.GetVehicleAsync(getRequest);

            Console.WriteLine("\nVehicle Details");

            Console.WriteLine($"Id       : {getReply.Vehicle.VehicleId}");
            Console.WriteLine($"Plate    : {getReply.Vehicle.PlateNumber}");
            Console.WriteLine($"Type     : {getReply.Vehicle.VehicleType}");
            Console.WriteLine($"Capacity : {getReply.Vehicle.CapacityKg}");
            Console.WriteLine($"Active   : {getReply.Vehicle.IsActive}");

            Console.ReadLine();
        }
    }
}
