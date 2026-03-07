using System.ComponentModel.DataAnnotations;

namespace Task.ViewModels
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
        public string Name { get; set; }
        [Range(18, 30, ErrorMessage = "Age must be between 18 and 30")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please select a department")]
        public int? DepartmentId { get; set; }
    }
}
