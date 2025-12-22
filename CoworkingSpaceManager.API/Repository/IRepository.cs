using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoworkingSpaceManager.API.Models;

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
        Task<bool> IsReserved(T a, DateTime date);
    }
}