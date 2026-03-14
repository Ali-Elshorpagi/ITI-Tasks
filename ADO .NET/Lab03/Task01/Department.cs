namespace Task01
{
    public class Department
    {
        public int Id { get; set; }
        public int Capacity { get; set; }
        public string Name { get; set; } = null!;
        public override string ToString() => $"Id: {Id}, Name: {Name}, Capacity: {Capacity}";
    }
}
