using System.ComponentModel.DataAnnotations;

namespace Task.Models
{
    public class Course
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [MaxLength(100)]
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Duration { get; set; }
    }
}
