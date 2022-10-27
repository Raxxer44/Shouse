using System.ComponentModel.DataAnnotations;

namespace Technic.Web.Areas.Account.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
