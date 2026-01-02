using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CoworkingSpaceManager.API.DTOs;
using CoworkingSpaceManager.API.Pagination;
using CoworkingSpaceManager.API.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoworkingSpaceManager.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IValidator<BookingPostDto> _bookingPostValidator;

        public BookingController(IBookingService bookingService,
            IValidator<BookingPostDto> bookingPostValidator)
        {
            _bookingService = bookingService;
            _bookingPostValidator = bookingPostValidator;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetBookings([FromQuery] PaginationParams paginationParams)
        {
           var bookings = await _bookingService.GetBookings(paginationParams);

           return Ok(bookings); 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            if (id <= 0)
                return BadRequest("The id have to be greater than 0");

            var booking = await _bookingService.GetBookingById(id);

            if (booking == null)
                return NotFound();

            return Ok(booking);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(BookingPostDto dto)
        {
            var validation = _bookingPostValidator.Validate(dto);

            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("The user could not be identified from the token");

            var booking = await _bookingService.CreateBooking(dto, userId);

            if (booking == null)
                return Conflict("The space does not exist or is already booked for that date");

            return CreatedAtAction(nameof(GetBookingById), new { id = booking.Id}, booking);
        }

        [HttpGet("my-bookings")]
        public async Task<IActionResult> GetMyBookings()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("The user could not be identified from the token");

            var bookings = await _bookingService.GetBookings(userId);

            return Ok(bookings);
        }

        [HttpDelete("{bookingId}")]
        public async Task<IActionResult> DeleteBooking(int bookingId)
        {
            if (bookingId <= 0)
                return BadRequest("The bookingId have to be greater than 0");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("The user could not be identified from the token");

            var booking = await _bookingService.DeleteBooking(bookingId, userId);

            if (booking == null)
                return Forbid();

            return Ok(booking);
        }
    }
}