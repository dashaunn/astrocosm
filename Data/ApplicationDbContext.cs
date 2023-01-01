using System;
using System.Collections.Generic;
using System.Text;
using Astrocosm.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Astrocosm.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<ZodiacSign> ZodiacSigns { get; set; }

        public DbSet<SunInfo> SunData { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}

