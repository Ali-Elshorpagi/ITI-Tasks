using Microsoft.EntityFrameworkCore;

namespace Lab03.Models
{
    public partial class ITIContext : DbContext
    {
        public ITIContext() { }
        public ITIContext(DbContextOptions<ITIContext> options) : base(options) { }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.DeptId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.StId).ValueGeneratedNever();
                entity.Property(e => e.StLname).IsFixedLength();

                entity.HasOne(d => d.Dept).WithMany(p => p.Students).HasConstraintName("FK_Student_Department");

                entity.HasOne(d => d.StSuperNavigation).WithMany(p => p.InverseStSuperNavigation).HasConstraintName("FK_Student_Student");
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}