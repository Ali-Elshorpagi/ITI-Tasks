using Microsoft.EntityFrameworkCore;
using Task01.Data;
using Task01.Hubs;
using Task01.Services;
using Task01.Services.Abstracts;

namespace Task01
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                    options.JsonSerializerOptions.ReferenceHandler =
                        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
            builder.Services.AddSignalR();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options
                    .UseLazyLoadingProxies()
                    .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddTransient<IStudentService, StudentService>();
            builder.Services.AddTransient<IDepartmentService, DepartmentService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            var defaultFiles = new DefaultFilesOptions();
            defaultFiles.DefaultFileNames.Clear();
            defaultFiles.DefaultFileNames.Add("students.html");
            app.UseDefaultFiles(defaultFiles);

            app.UseStaticFiles();
            app.UseAuthorization();
            app.MapControllers();
            app.MapHub<StudentHub>("/hubs/student");

            app.Run();
        }
    }
}
