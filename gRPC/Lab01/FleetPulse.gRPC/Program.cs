using FleetPulse.gRPC.Models;
using FleetPulse.gRPC.Services;

namespace FleetPulse.gRPC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddGrpc();

            builder.Services.AddSingleton<OrderStore>();
            builder.Services.AddSingleton<VehicleStore>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.MapGrpcService<FleetOperationsService>();
            app.MapGrpcService<VehicleService>();

            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}
