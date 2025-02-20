using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace KbcProject.Models
{
    public class LoginViewModel
    {
        [Key]
        public int LoginViewModelID { get; set; }//poxi rakheko
        [Required(ErrorMessage = "Username or Email is required")]
        [MaxLength(20, ErrorMessage = "Max 20 character is allowed.")]
        // for space between UserName and Space
        [DisplayName("Username or Email")]
        public string UserNameOrEmail { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Max 20 or min 5 character allowed.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
