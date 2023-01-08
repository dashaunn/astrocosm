using System;
using Microsoft.AspNetCore.Identity;

namespace Astrocosm.Models
{
    public class AppUser : IdentityUser
    {
        public DateTime DOB { get; set; }

        public int SunSignId { get; set; }

        public virtual ZodiacSign SunSign { get; set; }
    }
}