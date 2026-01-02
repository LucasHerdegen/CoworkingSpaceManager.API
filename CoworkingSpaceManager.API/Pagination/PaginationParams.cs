using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoworkingSpaceManager.API.Pagination
{
    public class PaginationParams
    {
        private const int MaxPageSize = 50;

        private int _pageNumber = 1;
        public int PageNumber
        {
            get => _pageNumber; 
            set => _pageNumber = Math.Max(1, value);
        }

        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = Math.Clamp(value, 1, MaxPageSize);
        }
    }
}