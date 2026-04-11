using System.ComponentModel.DataAnnotations;

namespace Task01.DTOs.Department;

public class DepartmentUpsertDto
{
    [MaxLength(100)]
    public string Name { get; set; }
    [MaxLength(250)]
    public string? Description { get; set; }
    [MaxLength(100)]
    public string? Location { get; set; }
    public int? Manager { get; set; }
    public DateTime? ManagerHireDate { get; set; }
}

