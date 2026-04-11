using System.ComponentModel.DataAnnotations;

namespace Task01.DTOs.Student;

public class StudentUpsertDto
{
    [MaxLength(100)]
    public string FirstName { get; set; }
    [MaxLength(100)]
    public string? LastName { get; set; }
    [MaxLength(200)]
    public string? Address { get; set; }
    [Range(1, 100)]
    public int Age { get; set; }
    [Required]
    public int DepartmentId { get; set; }
    public int? SupervisorId { get; set; }
}

