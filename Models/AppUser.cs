using System;
using Microsoft.AspNetCore.Identity;

namespace Astrocosm.Models
{
    public class AppUser : IdentityUser
    {
        public DateTime DOB { get; set; }

        public ZodiacSign SunSign { get; set; }
    }
}