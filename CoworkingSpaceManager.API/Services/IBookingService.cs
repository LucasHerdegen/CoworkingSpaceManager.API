using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoworkingSpaceManager.API.DTOs;
using CoworkingSpaceManager.API.Pagination;

namespace CoworkingSpaceManager.API.Services
{
    public interface IBookingService
    {
        Task<PagedList<BookingDto>> GetBookings(PaginationParams paginationParams);
        Task<IEnumerable<BookingDto>> GetBookings(string userId);
        Task<BookingDto?> GetBookingById(int id);
        Task<BookingDto?> CreateBooking(BookingPostDto dto, string userId);
        Task<BookingDto?> DeleteBooking(int id, string userId);
    }
}