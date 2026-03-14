namespace Task01
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public override string ToString() => $"Category ID : {Id}\nCategory Name : {Name} \nCategory Description : {Description}";
    }
}
