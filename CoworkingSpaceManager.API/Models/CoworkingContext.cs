using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoworkingSpaceManager.API.Models
{
    public class CoworkingContext : IdentityDbContext<IdentityUser>
    {
        public CoworkingContext(DbContextOptions<CoworkingContext> options) : base(options)
        {
            
        }

        public DbSet<Space> Spaces { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}