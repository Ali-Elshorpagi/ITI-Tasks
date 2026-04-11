namespace Task01.DTOs.Student;

public class StudentReadDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public int Age { get; set; }
    public string DepartmentName { get; set; }
    public string? SupervisorName { get; set; }
}

