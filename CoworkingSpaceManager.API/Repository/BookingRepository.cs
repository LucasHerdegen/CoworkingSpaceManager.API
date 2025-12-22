using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoworkingSpaceManager.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CoworkingSpaceManager.API.Repository
{
    public class BookingRepository : IRepository<Booking>
    {
        private readonly CoworkingContext _context;

        public BookingRepository(CoworkingContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Booking>> Get() =>
            await _context.Bookings.ToListAsync();

        public async Task<Booking?> GetById(int id) =>
            await _context.Bookings.FirstOrDefaultAsync(x => x.Id == id);

        public async Task Create(Booking booking) =>
            await _context.Bookings.AddAsync(booking);

        public void Delete(Booking booking) =>
            _context.Bookings.Remove(booking);

        public void Update(Booking booking) =>
            _context.Bookings.Update(booking);

        public async Task Save() =>
            await _context.SaveChangesAsync();

        public Task<bool> IsReserved(Booking booking, DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}