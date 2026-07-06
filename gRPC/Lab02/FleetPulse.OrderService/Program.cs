using FleetPulse.OrderService.Advanced.Interceptors;
using FleetPulse.OrderService.Advanced.Models;
using FleetPulse.OrderService.Advanced.Services;
using FleetPulse.OrderService.Data;
using FleetPulse.OrderService.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FleetPulse.OrderService.Advanced
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddGrpc(options =>
            {
                options.Interceptors.Add<LoggingInterceptor>();
                options.Interceptors.Add<ExceptionInterceptor>();
            });

            builder.Services.AddSingleton<VehicleStore>();

            builder.Services.AddSingleton<LoggingInterceptor>();
            builder.Services.AddSingleton<ExceptionInterceptor>();

            builder.Services.AddScoped<IOrderRepository, OrderRepository>();

            builder.Services.AddDbContext<FleetDbContext>(options =>
            {
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],

                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
                    };
                });

            builder.Services.AddAuthorization();


            var app = builder.Build();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapGrpcService<FleetOperationsService>();
            app.MapGrpcService<VehicleService>();


            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}
