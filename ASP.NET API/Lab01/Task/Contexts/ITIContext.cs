using Microsoft.EntityFrameworkCore;
using Task.Models;

namespace Task.Contexts
{
    public class ITIContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public ITIContext(DbContextOptions<ITIContext> options) : base(options) { }
        public ITIContext() { }
    }
}
