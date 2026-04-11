namespace Lab03.DTOs
{
    public class DepartmentDTO
    {
        public int DeptId { get; set; }

        public string DeptName { get; set; }

        public string DeptDesc { get; set; }
        public string DeptLocation { get; set; }
        public DateOnly? ManagerHiredate { get; set; }
        public int StdCount { get; set; }
    }
}
