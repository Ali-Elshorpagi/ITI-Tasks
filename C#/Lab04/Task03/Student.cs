namespace Task03
{
    internal class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Student(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public void WhenExamStarted(string msg) => Console.WriteLine($"Student {Name}, notified: {msg}");
    }
}
