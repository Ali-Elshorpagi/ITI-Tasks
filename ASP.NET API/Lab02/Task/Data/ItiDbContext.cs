using Microsoft.EntityFrameworkCore;
using Task01.Models;

namespace Task01.Data;

public class ItiDbContext : DbContext
{
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Student> Students => Set<Student>();
    public ItiDbContext(DbContextOptions<ItiDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("Department");
            entity.HasKey(d => d.Id);

            entity.Property(d => d.Id).HasColumnName("Dept_Id");
            entity.Property(d => d.Name).HasColumnName("Dept_Name");
            entity.Property(d => d.Description).HasColumnName("Dept_Desc");
            entity.Property(d => d.Location).HasColumnName("Dept_Location");
            entity.Property(d => d.Manager).HasColumnName("Dept_Manager");
            entity.Property(d => d.ManagerHireDate).HasColumnName("Manager_hiredate");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("Student");
            entity.HasKey(s => s.Id);

            entity.Property(s => s.Id).HasColumnName("St_Id");
            entity.Property(s => s.FirstName).HasColumnName("St_Fname");
            entity.Property(s => s.LastName).HasColumnName("St_Lname");
            entity.Property(s => s.Address).HasColumnName("St_Address");
            entity.Property(s => s.Age).HasColumnName("St_Age");
            entity.Property(s => s.DepartmentId).HasColumnName("Dept_Id");
            entity.Property(s => s.SupervisorId).HasColumnName("St_super");

            entity.HasOne(s => s.Department)
                .WithMany(d => d.Students)
                .HasForeignKey(s => s.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(s => s.Supervisor)
                .WithMany(s => s.Supervisees)
                .HasForeignKey(s => s.SupervisorId)
                .OnDelete(DeleteBehavior.SetNull);
        });
    }
}

