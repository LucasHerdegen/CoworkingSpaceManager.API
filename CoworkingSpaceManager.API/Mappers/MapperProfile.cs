using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoworkingSpaceManager.API.DTOs;
using CoworkingSpaceManager.API.Models;

namespace CoworkingSpaceManager.API.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<RegisterDto, ApplicationUser>()
                .ForMember(user => user.UserName, config => config.MapFrom(dto => dto.Email));

            CreateMap<Space, SpaceDto>();
            CreateMap<SpacePostDto, Space>();
            CreateMap<SpacePutDto, Space>();
        }
    }
}