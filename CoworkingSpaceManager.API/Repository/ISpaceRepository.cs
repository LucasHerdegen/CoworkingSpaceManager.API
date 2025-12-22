using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoworkingSpaceManager.API.Models;

namespace CoworkingSpaceManager.API.Repository
{
    public interface ISpaceRepository : IRepository<Space>
    {
        Task<bool> IsReserved(Space space, DateTime date);
    }
}