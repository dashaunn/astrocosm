using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Astrocosm.Models
{
	public class LoginViewModel
	{
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

        public LoginViewModel()
		{
		}
	}
}

