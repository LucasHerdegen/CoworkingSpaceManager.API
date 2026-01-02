using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoworkingSpaceManager.API.Models;
using CoworkingSpaceManager.API.Pagination;

namespace CoworkingSpaceManager.API.Repository
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> Get();
        Task<T?> GetById(int id);
        Task Create(T create);
        void Update(T update);
        void Delete(T delete);
        Task Save();
        Task<PagedList<T>> GetPaged(PaginationParams paginationParams);
    }
}