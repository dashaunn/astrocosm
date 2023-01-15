using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Astrocosm.Models
{
	public class RegisterViewModel
	{
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Password must be between 6 and 20 characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and confirmation password not match.")]
        public string ConfirmPassword { get; set; }

        public RegisterViewModel()
		{
		}
	}
}

