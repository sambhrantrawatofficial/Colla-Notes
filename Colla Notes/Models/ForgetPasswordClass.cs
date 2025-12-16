using System.ComponentModel.DataAnnotations;

namespace Colla_Notes.Models
{
    public class ForgetPasswordClass
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
    }
}
