namespace Task01.DTOs.Department;

public class DepartmentReadDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
    public int? Manager { get; set; }
    public DateTime? ManagerHireDate { get; set; }
    public int StudentCount { get; set; }
}

