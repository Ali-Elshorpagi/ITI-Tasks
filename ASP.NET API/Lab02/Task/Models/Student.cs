using System.ComponentModel.DataAnnotations;

namespace Task01.Models;

public class Student
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string FirstName { get; set; }
    [MaxLength(100)]
    public string? LastName { get; set; }
    [MaxLength(200)]
    public string? Address { get; set; }
    [Range(1, 100)]
    public int Age { get; set; }
    public int DepartmentId { get; set; }
    public Department? Department { get; set; }
    public int? SupervisorId { get; set; }
    public Student? Supervisor { get; set; }
    public ICollection<Student> Supervisees { get; set; } = new List<Student>();
}

