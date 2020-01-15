using System;
using System.Collections.Generic;
using System.Text;
using AbsenceWebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AbsenceWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Absence> Absence { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
    }
}
