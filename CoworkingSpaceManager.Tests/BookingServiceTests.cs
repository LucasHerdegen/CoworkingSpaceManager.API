using AutoMapper;
using CoworkingSpaceManager.API.DTOs;
using CoworkingSpaceManager.API.Models;
using CoworkingSpaceManager.API.Repository;
using CoworkingSpaceManager.API.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoworkingSpaceManager.Tests
{
    public class BookingServiceTests
    {
        private readonly Mock<IBookingRepository> _mockBookingRepo;
        private readonly Mock<ISpaceRepository> _mockSpaceRepo;
        private readonly Mock<IMapper> _mockMapper;

        private readonly BookingService _service;

        public BookingServiceTests()
        {
            _mockBookingRepo = new Mock<IBookingRepository>();
            _mockSpaceRepo = new Mock<ISpaceRepository>();
            _mockMapper = new Mock<IMapper>();

            _service = new BookingService(
                _mockMapper.Object, 
                _mockBookingRepo.Object, 
                _mockSpaceRepo.Object
            );
        }

        [Fact]
        public async Task CreateBooking_DeberiaCrearReserva_CuandoEspacioDisponibleYNoReservado()
        {
            // arrange
            int spaceId = 100;
            string userId = "ABCDEFG";
            var date = DateTime.Now.AddDays(1);

            var space = new Space{Id = spaceId, Available = true, Capacity = 6, Name = "D15"};
            var bookingDto = new BookingDto{UserId = userId, ReservationDate = date, SpaceId = spaceId};
            var bookingPostDto = new BookingPostDto{SpaceId = spaceId, ReservationDate = date};

            _mockSpaceRepo.Setup(repo => repo.GetById(spaceId))
                .ReturnsAsync(space);
            _mockSpaceRepo.Setup(repo => repo.IsReserved(space, date))
                .ReturnsAsync(false);
            _mockMapper.Setup(m => m.Map<BookingDto>(It.Is<Booking>(b => 
                b.SpaceId == spaceId && 
                b.UserId == userId && 
                b.ReservationDate == date
            )))
            .Returns(bookingDto);

            // act
            var result = await _service.CreateBooking(bookingPostDto, userId);

            // assert
            result.Should().NotBeNull();
            _mockBookingRepo.Verify(repo => repo.Create(It.Is<Booking>(b => 
                b.SpaceId == spaceId && 
                b.UserId == userId
            )), Times.Once);
            _mockBookingRepo.Verify(repo => repo.Save(), Times.Once);
        }

        [Fact]
        public async Task CreateBooking_DeberiaRetornarNull_CuandoLaFechaEstaReservada()
        {
            // arrange
            int spaceId = 100;
            string userId = "ABCDEFG";
            var date = DateTime.Now.AddDays(1);

            var space = new Space{Id = spaceId, Available = true, Capacity = 6, Name = "D15"};
            var bookingPostDto = new BookingPostDto{SpaceId = spaceId, ReservationDate = date};

            _mockSpaceRepo.Setup(repo => repo.GetById(spaceId))
                .ReturnsAsync(space);
            _mockSpaceRepo.Setup(repo => repo.IsReserved(space, date))
                .ReturnsAsync(true);

            // act
            var result = await _service.CreateBooking(bookingPostDto, userId);

            // assert
            result.Should().BeNull();
            _mockSpaceRepo.Verify(repo => repo.GetById(spaceId), Times.Once);
            _mockSpaceRepo.Verify(repo => repo.IsReserved(space, date), Times.Once);
            _mockBookingRepo.Verify(repo => repo.Save(), Times.Never);
        }

        [Fact]
        public async Task DeleteBooking_DeberiaRetornarNull_CuandoElUsuarioNoEsElDuenio()
        {
            // arrange
            int bookingId = 100;
            string userBookingId = "12345";
            string myUserId = "ABCDEF";
            var booking = new Booking{Id = bookingId, ReservationDate = DateTime.Now.AddDays(1), UserId = userBookingId};

            _mockBookingRepo.Setup(repo => repo.GetById(bookingId))
                .ReturnsAsync(booking);

            // act
            var result = await _service.DeleteBooking(bookingId, myUserId);

            // assert
            result.Should().BeNull();
            _mockBookingRepo.Verify(repo => repo.GetById(bookingId), Times.Once);
            _mockBookingRepo.Verify(repo => repo.Delete(booking), Times.Never);
            _mockBookingRepo.Verify(repo => repo.Save(), Times.Never);
        }
    }
}