using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoworkingSpaceManager.API.DTOs;
using CoworkingSpaceManager.API.Pagination;

namespace CoworkingSpaceManager.API.Services
{
    public interface ISpaceService
    {
        Task<PagedList<SpaceDto>> GetSpaces(PaginationParams paginationParams);
        Task<SpaceDto?> GetSpaceById(int id);
        Task<SpaceDto> CreateSpace(SpacePostDto dto);
        Task<SpaceDto?> UpdateSpace(int id, SpacePutDto dto);
        Task<SpaceDto?> DeleteSpace(int id);
    }
}