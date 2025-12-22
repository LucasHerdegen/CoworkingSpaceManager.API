using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoworkingSpaceManager.API.DTOs;
using CoworkingSpaceManager.API.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoworkingSpaceManager.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SpacesController : ControllerBase
    {
        private readonly ISpaceService _spaceService;
        private readonly IValidator<SpacePostDto> _spacePostValidator;
        public SpacesController(ISpaceService spaceService,
            IValidator<SpacePostDto> spacePostValidator)
        {
            _spaceService = spaceService;
            _spacePostValidator = spacePostValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetSpaces()
        {
            var spaces = await _spaceService.GetSpaces();

            return Ok(spaces);            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSpaceById(int id)
        {
            if (id <= 0)
                return BadRequest("The id have to be greater than 0");

            var space = await _spaceService.GetSpaceById(id);

            if (space == null)
                return NotFound();

            return Ok(space);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSpace(SpacePostDto dto)
        {
            var validation = _spacePostValidator.Validate(dto);

            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            var space = await _spaceService.CreateSpace(dto);

            return CreatedAtAction(nameof(GetSpaceById), new { id = space.Id}, space);
        }
    }
}