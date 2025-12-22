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
        private readonly IValidator<SpacePutDto> _spacePutValidator;
        public SpacesController(ISpaceService spaceService,
            IValidator<SpacePostDto> spacePostValidator,
            IValidator<SpacePutDto> spacePutValidator)
        {
            _spaceService = spaceService;
            _spacePostValidator = spacePostValidator;
            _spacePutValidator = spacePutValidator;
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSpace(int id, SpacePutDto dto)
        {
            if (id <= 0)
                return BadRequest("The id have to be greater than 0");

            var validation = _spacePutValidator.Validate(dto);

            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            var space = await _spaceService.UpdateSpace(id, dto);

            if (space == null)
                return NotFound();

            return Ok(space);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSpace(int id)
        {
            if (id <= 0)
                return BadRequest("The id have to be greater than 0");

            var space = _spaceService.DeleteSpace(id);

            if (space == null)
                return NotFound();

            return Ok(space);
        }
    }
}