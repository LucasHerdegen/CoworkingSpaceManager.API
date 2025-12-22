using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoworkingSpaceManager.API.DTOs;
using CoworkingSpaceManager.API.Models;
using CoworkingSpaceManager.API.Repository;

namespace CoworkingSpaceManager.API.Services
{
    public class BookingService : IBookingService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Booking> _bookingRepository;

        public BookingService(IMapper mapper, IRepository<Booking> bookingRepository)
        {
            _mapper = mapper;
            _bookingRepository = bookingRepository;
        }

        public async Task<IEnumerable<BookingDto>> GetBookings()
        {
            var bookings = await _bookingRepository.Get();

            var bookingDtos = _mapper.Map<IEnumerable<BookingDto>>(bookings);

            return bookingDtos;
        }

        public async Task<BookingDto?> GetBookingById(int id)
        {
            var booking = await _bookingRepository.GetById(id);

            if (booking == null)
                return null;

            var bookingDto = _mapper.Map<BookingDto>(booking);

            return bookingDto;
        }
    }
}