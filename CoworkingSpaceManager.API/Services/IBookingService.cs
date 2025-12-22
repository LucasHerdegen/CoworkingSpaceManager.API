using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoworkingSpaceManager.API.DTOs;

namespace CoworkingSpaceManager.API.Services
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingDto>> GetBookings();
        Task<BookingDto?> GetBookingById(int id);
       Task<BookingDto?> CreateBooking(BookingPostDto dto, string userId);
    }
}