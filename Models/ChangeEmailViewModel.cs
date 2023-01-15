using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Astrocosm.Models
{
	public class ChangeEmailViewModel
	{
        [Required]
        [EmailAddress]
        [Display(Name = "New Email")]
        public string NewEmail { get; set; }

        public ChangeEmailViewModel()
		{
		}
	}
}

