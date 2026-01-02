using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoworkingSpaceManager.API.Models;
using CoworkingSpaceManager.API.Pagination;
using Microsoft.EntityFrameworkCore;

namespace CoworkingSpaceManager.API.Repository
{
    public class SpaceRepository : ISpaceRepository
    {
        private readonly CoworkingContext _context;

        public SpaceRepository(CoworkingContext context)
        {
            _context = context;
        }

        public async Task Create(Space space) =>
            await _context.Spaces.AddAsync(space);

        public async Task<Space?> GetById(int id) =>
            await _context.Spaces.FirstOrDefaultAsync(x => x.Id == id);

        public void Delete(Space space) =>
            _context.Spaces.Remove(space);

        public async Task<IEnumerable<Space>> Get() =>
            await _context.Spaces.ToListAsync();

        public void Update(Space space) =>
            _context.Spaces.Update(space);

        public async Task Save() =>
            await _context.SaveChangesAsync();

        public async Task<bool> IsReserved(Space space, DateTime date) =>
            await _context.Bookings.AnyAsync(x => x.SpaceId == space.Id && x.ReservationDate.Date == date.Date);

        public async Task<PagedList<Space>> GetPaged(PaginationParams paginationParams)
        {
            var query = _context.Set<Space>().AsQueryable();

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(x => x.Id)
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize)
                .ToListAsync();

            return new PagedList<Space>(items, totalCount, paginationParams.PageNumber, paginationParams.PageSize);
        }
    }
}