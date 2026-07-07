using Microsoft.EntityFrameworkCore;
using Task01.Models;

namespace Task01.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .HasMany(x => x.Students)
                .WithOne(x => x.Department)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
