using System.ComponentModel.DataAnnotations;

namespace TheChat.Models.DTO
{
    public class UserRegisterDTO
    {
        [MaxLength(30, ErrorMessage = "First name is too long")]
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; } = String.Empty;


        [MaxLength(30, ErrorMessage = "Second name is too long")]
        [Required(ErrorMessage = "Second Name is required")]
        public string SecondName { get; set; } = String.Empty;


        [MaxLength(30, ErrorMessage = "Login is too long")]
        [Required(ErrorMessage = "Login is required")]
        public string Login { get; set; } = String.Empty;


        [EmailAddress(ErrorMessage = "Invalid address")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = String.Empty;


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = String.Empty;


        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords are different")]
        [Required(ErrorMessage = "Password repeat is required")]
        public string ConfirmPassword { get; set; } = String.Empty;
    }
}
