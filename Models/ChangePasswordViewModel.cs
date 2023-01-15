using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Astrocosm.Models
{
	public class ChangePasswordViewModel
	{
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Password must be between 6 and 20 characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public ChangePasswordViewModel()
		{
		}
	}
}

