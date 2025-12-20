using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoworkingSpaceManager.API.DTOs;
using Microsoft.AspNetCore.Identity;

namespace CoworkingSpaceManager.API.Services
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterUser(RegisterDto dto);
    }
}