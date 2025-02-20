using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace KbcProject.Entities
{
   
   
        [Index(nameof(Email), IsUnique = true)]
        [Index(nameof(UserName), IsUnique = true)]

        public class UserAccount

    { 
            [Key]
            public int id { get; set; }
            [Required(ErrorMessage = "First name is required")]
            [MaxLength(50, ErrorMessage = "Max 50 character is allowed.")]
            public string FirstName { get; set; }
            [Required(ErrorMessage = "Last name is required")]
            [MaxLength(50, ErrorMessage = "Max 50 character is allowed.")]
            public string LastName { get; set; }
            [Required(ErrorMessage = "Email is required")]
            [MaxLength(100, ErrorMessage = "Max 100 character is allowed.")]

            public string Email { get; set; }
            [Required(ErrorMessage = "Username is required")]
            [MaxLength(20, ErrorMessage = "Max 20 character is allowed.")]
            public string UserName { get; set; }
            [Required(ErrorMessage = "Password is required")]
            [MaxLength(20, ErrorMessage = "Max 20 character is allowed.")]

            public string Password { get; set; }
        }
}
