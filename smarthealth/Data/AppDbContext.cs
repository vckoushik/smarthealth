using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using smarthealth.Models;

namespace smarthealth.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
    }
}
