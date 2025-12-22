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
        private readonly IRepository<Space> _spaceRepository;

        public BookingService(IMapper mapper,
            IRepository<Booking> bookingRepository,
            IRepository<Space> spaceRepository)
        {
            _mapper = mapper;
            _bookingRepository = bookingRepository;
            _spaceRepository = spaceRepository;
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

        public async Task<BookingDto?> CreateBooking(BookingPostDto dto, string userId)
        {
            var space = await _spaceRepository.GetById(dto.SpaceId);

            if (space == null || !space.Available)
                return null;

            var isReserved = await _spaceRepository.IsReserved(space, dto.ReservationDate);

            if (isReserved)
                return null;

            var newBooking = new Booking
            {
                UserId = userId,
                SpaceId = dto.SpaceId,
                ReservationDate = dto.ReservationDate
            };

            await _bookingRepository.Create(newBooking);
            await _bookingRepository.Save();

            var bookingDto = _mapper.Map<BookingDto>(newBooking);

            return bookingDto;
        }
    }
}