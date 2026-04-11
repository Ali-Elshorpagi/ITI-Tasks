
using Lab03.Config;
using Lab03.Models;
using Lab03.UoW;
using Microsoft.EntityFrameworkCore;

namespace Lab03
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddXmlSerializerFormatters();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

            builder.Services.AddDbContext<ITIContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
            builder.Services.AddSwaggerGen();
            builder.Services.AddOpenApi();

            builder.Services.AddAutoMapper(cfg => cfg.AddProfile<AutoMapperConfig>());
            builder.Services.AddScoped<UnitOFWork>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
