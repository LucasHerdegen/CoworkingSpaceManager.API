using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoworkingSpaceManager.API.DTOs;
using CoworkingSpaceManager.API.Models;
using CoworkingSpaceManager.API.Repository;

namespace CoworkingSpaceManager.API.Services
{
    public class SpaceService : ISpaceService
    {
        private readonly IRepository<Space> _spaceRepository;
        private readonly IMapper _mapper;

        public SpaceService(IRepository<Space> spaceRepository, IMapper mapper)
        {
            _spaceRepository = spaceRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<SpaceDto>> GetSpaces()
        {
            var spaces = await _spaceRepository.Get();

            var spacesDtos = _mapper.Map<IEnumerable<SpaceDto>>(spaces);

            return spacesDtos;
        }

        public async Task<SpaceDto?> GetSpaceById(int id)
        {
            var space = await _spaceRepository.GetById(id);

            if (space == null)
                return null;
            
            var spaceDto = _mapper.Map<SpaceDto>(space);

            return spaceDto;
        }

        public async Task CreateSpace(SpacePostDto dto)
        {
            var space = _mapper.Map<Space>(dto);

            await _spaceRepository.Create(space);
            await _spaceRepository.Save();
        }
    }
}