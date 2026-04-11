using Microsoft.AspNetCore.Identity;

namespace Lab04.Models
{
    public class Student: IdentityUser
    {
        public string Fullname { get; set; }
        public int Age { get; set; }
    }
}
