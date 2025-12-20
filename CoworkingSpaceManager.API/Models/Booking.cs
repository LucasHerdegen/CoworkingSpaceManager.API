using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CoworkingSpaceManager.API.Models
{
    public class Booking
    {
        public Guid UserId { get; set; }
        public IdentityUser? User { get; set; }
        public int SpaceId { get; set; }
        public DateTime ReservationDate { get; set; }
    }
}