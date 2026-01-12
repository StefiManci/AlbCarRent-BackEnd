using System.ComponentModel.DataAnnotations;

namespace AlbCarRent.Modules.AuthModule.DTOs
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Full-Name is required!")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Username is required!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Account type is required!")]
        
        public bool IsBussinessAccount { get; set; }
    }
}
