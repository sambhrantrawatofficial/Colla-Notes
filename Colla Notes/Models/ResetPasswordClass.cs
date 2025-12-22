using System.ComponentModel.DataAnnotations;

namespace Colla_Notes.Models
{
    public class ResetPasswordClass
    {
        [Required(ErrorMessage = "New password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string? New_Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("New_Password", ErrorMessage = "Passwords do not match")]
        public string? Confirm_Password { get; set; }
    }
}
