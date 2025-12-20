using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoworkingSpaceManager.API.DTOs;
using CoworkingSpaceManager.API.Models;
using CoworkingSpaceManager.API.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoworkingSpaceManager.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IValidator<RegisterDto> _registerValidator;
        private readonly IUserService _userService;

        public AuthController(IValidator<RegisterDto> registerValidator, IUserService userService)
        {
            _registerValidator = registerValidator;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var validation = _registerValidator.Validate(dto);

            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            var result = await _userService.RegisterUser(dto);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Successfully registered");
        }
    }
}