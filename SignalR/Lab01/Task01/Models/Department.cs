using System.ComponentModel.DataAnnotations;

namespace Task01.Models
{
    public class Department
    {
        public int Id { get; set; }
        [MaxLength(10)]
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Student> Students { get; set; } = new HashSet<Student>();
    }
}
