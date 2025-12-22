using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoworkingSpaceManager.API.DTOs
{
    public class BookingPostDto
    {
        public int SpaceId { get; set; }
        public DateTime ReservationDate { get; set; }
    }
}