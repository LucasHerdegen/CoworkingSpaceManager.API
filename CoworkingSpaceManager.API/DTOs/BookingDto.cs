using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoworkingSpaceManager.API.DTOs
{
    public class BookingDto
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public int SpaceId { get; set; }
        public DateTime ReservationDate { get; set; }
    }
}