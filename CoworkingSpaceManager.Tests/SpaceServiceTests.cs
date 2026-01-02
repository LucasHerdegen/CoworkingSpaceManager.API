using AutoMapper;
using CoworkingSpaceManager.API.DTOs;
using CoworkingSpaceManager.API.Models;
using CoworkingSpaceManager.API.Repository;
using CoworkingSpaceManager.API.Services;
using FluentAssertions; // Librería para asserts más legibles
using Moq; // Librería para crear objetos falsos
using Xunit; // El framework de testing

namespace CoworkingSpaceManager.Tests
{
    public class SpaceServiceTests
    {
        private readonly Mock<ISpaceRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        
        private readonly SpaceService _service;

        public SpaceServiceTests()
        {
            _mockRepo = new Mock<ISpaceRepository>();
            _mockMapper = new Mock<IMapper>();

            _service = new SpaceService(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetSpaceById_DeberiaRetornarDto_CuandoElEspacioExiste()
        {
            // arrange
            int id = 100;
            var space = new Space{Id = id, Available = true, Capacity = 4, Name = "Oficina-4D"};
            var spaceDto = new SpaceDto{Id = id, Available = true, Capacity = 4, Name = "Oficina-4D"};

            _mockRepo.Setup(repo => repo.GetById(id))
                .ReturnsAsync(space);
            _mockMapper.Setup(m => m.Map<SpaceDto>(space))
                .Returns(spaceDto);

            // act
            var result = await _service.GetSpaceById(id);

            // assert
            result.Should().NotBeNull();
            result.Should().Be(spaceDto);

            _mockRepo.Verify(repo => repo.GetById(id), Times.Once);
            _mockRepo.Verify(repo => repo.Save(), Times.Never);

        }

        [Fact]
        public async Task GetSpaceById_DeberiaRetornarNull_CuandoElEspacioNoExiste()
        {
            // arrange
            int id = 100;
            _mockRepo.Setup(repo => repo.GetById(id))
                .ReturnsAsync((Space?)null);

            // act
            var result = await _service.GetSpaceById(id);

            // assert
            result.Should().BeNull();
            _mockRepo.Verify(repo => repo.GetById(id), Times.Once);
            _mockRepo.Verify(repo => repo.Save(), Times.Never);
        }

        [Fact]
        public async Task DeleteSpace_DeberiaLlamarAlRepositorio_CuandoElIdExiste()
        {
            // arrange
            int id = 100;
            var space = new Space{Available = true, Capacity = 4, Id = id, Name = "Oficina-15A"};
            var spaceDto = new SpaceDto{Available = true, Capacity = 4, Id = id, Name = "Oficina-15A"};
            
            _mockRepo.Setup(repo => repo.GetById(id))
                .ReturnsAsync(space);
            _mockMapper.Setup(m => m.Map<SpaceDto>(space))
                .Returns(spaceDto);

            // act
            var result = await _service.DeleteSpace(id);

            // assert
            result.Should().NotBeNull();
            _mockRepo.Verify(repo => repo.Delete(space), Times.Once);
            _mockRepo.Verify(repo => repo.Save(), Times.Once);
        }
    }
}