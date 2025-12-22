using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoworkingSpaceManager.API.DTOs;

namespace CoworkingSpaceManager.API.Services
{
    public interface ISpaceService
    {
        Task<IEnumerable<SpaceDto>> GetSpaces();
        Task<SpaceDto?> GetSpaceById(int id);
        Task<SpaceDto> CreateSpace(SpacePostDto dto);
        Task<SpaceDto?> UpdateSpace(int id, SpacePutDto dto);
    }
}