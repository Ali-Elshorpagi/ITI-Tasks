
using FleetPulse.API.Repos;
using FleetPulse.API.Repositories;
using FleetPulse.OrderService.Data;
using Grpc.Core;
using Grpc.Net.Client.Configuration;
using FleetPulse.OrderService.Protos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FleetPulse.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddScoped<IUserRepo, UserRepo>();

            builder.Services.AddHttpContextAccessor();

            var serviceConfig = new ServiceConfig
            {
                MethodConfigs =
                {
                    new MethodConfig
                    {
                        Names = { MethodName.Default },
                        RetryPolicy = new RetryPolicy
                        {
                            MaxAttempts         = 5,
                            InitialBackoff      = TimeSpan.FromSeconds(1),
                            MaxBackoff          = TimeSpan.FromSeconds(5),
                            BackoffMultiplier   = 1.5,
                            RetryableStatusCodes = { StatusCode.Unavailable }
                        }
                    }
                }
            };

            builder.Services.AddGrpcClient<FleetOperationsService.FleetOperationsServiceClient>(options =>
            {
                options.Address = new Uri("https://localhost:7172");
            })
            .ConfigureChannel(options =>
            {
                options.ServiceConfig = serviceConfig;
            })
            .AddCallCredentials((context, metadata, serviceProvider) =>
            {
                var accessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();

                var authHeader = accessor.HttpContext?
                    .Request.Headers["Authorization"].ToString();

                if (!string.IsNullOrWhiteSpace(authHeader) &&
                    authHeader.StartsWith("Bearer "))
                {
                    metadata.Add("Authorization", authHeader);
                }

                return Task.CompletedTask;
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

            builder.Services.AddDbContext<FleetDbContext>(options =>
            {
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddAuthorization();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
