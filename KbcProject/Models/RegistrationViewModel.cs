using System.ComponentModel.DataAnnotations;

namespace KbcProject.Models
{
    public class RegistrationViewModel
    {
        [Key]
        public int RegistrationViewModelID { get; set; }
        [Required(ErrorMessage = "First name is required")]
        [MaxLength(50, ErrorMessage = "Max 50 character is allowed.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(50, ErrorMessage = "Max 50 character is allowed.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [MaxLength(100, ErrorMessage = "Max 100 character is allowed.")]
        [EmailAddress(ErrorMessage = "please enter valid Email")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
        ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Username is required")]
        [MaxLength(20, ErrorMessage = "Max 20 character is allowed.")]

        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Max 20 or min 5 character allowed.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "please confim your password.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
