using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Task.Context;
using Task.Models;
using Task.Repositories;
using Task.Repositories.IRepositories;

namespace Task
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Logging.AddFilter("Microsoft.AspNetCore.Watch", LogLevel.None);

            var configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: false)
                   .Build();

            var connectionStringKey = Environment.MachineName.Equals("DESKTOP-LTQVC01", StringComparison.OrdinalIgnoreCase) ? "Hollow" : "Sukuna";
            var connectionString = configuration.GetConnectionString(connectionStringKey);

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddScoped<IRepository<Student>, StudentRepository>();
            builder.Services.AddScoped<IRepository<Department>, DepartmentRepository>();
            builder.Services.AddScoped<IRepository<Course>, CourseRepository>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
