using Microsoft.EntityFrameworkCore;
using Task01.Data;
using Task01.Profiles;
using Task01.Repositories;
using Task01.Repositories.Interfaces;

namespace Task01
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Logging.AddFilter("Microsoft.AspNetCore.Watch", LogLevel.None);

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ItiDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

            builder.Services.AddScoped<IStudentRepository, StudentRepository>();
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

            builder.Services.AddAutoMapper(op => op.AddProfile<MappingProfile>());

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
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

