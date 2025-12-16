using Greenhouse_API.DTOs;
using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net.WebSockets;

namespace Greenhouse_API.Services
{
    public class PlantService : IPlantService
    {
        private IRepository<Plant> _repository;
        private readonly ILogger<PlantService> _logger;
        private readonly ISpecimenService _specimenService;
        private readonly IZoneService _zoneService;

        public PlantService(IRepository<Plant> repository, ILogger<PlantService> logger, IZoneService zoneService, ISpecimenService specimenService)
        {
            _repository = repository;
            _logger = logger;
            _zoneService = zoneService;
            _specimenService = specimenService;
        }

        public async Task<IEnumerable<PlantDto>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all plants from the database.");
            var plants = await _repository.GetAllAsync();

            return plants.Select(plant => new PlantDto
            {
                Id = plant.Id,
                AcquiredDate = plant.AcquiredDate,
                SpecimenId = plant.SpecimenId,
                ZoneId = plant.ZoneId,
                MomId = plant.MomId,
                Description = plant.Description,
                IsActive = plant.IsActive,
                CreatedAt = plant.CreatedAt
            });
        }

        public async Task<PlantDto?> GetByIdAsync(int id)
        {
            var plant = await _repository.GetByIdAsync(id);
            if(plant == null)
            {
                _logger.LogWarning("Plant with ID {PlantId} not found.", id);
                return null;
            }

            _logger.LogInformation("Plant with ID {PlantId} retrieved successfully.", id);
            return new PlantDto
            {
                Id = plant.Id,
                AcquiredDate = plant.AcquiredDate,
                SpecimenId = plant.SpecimenId,
                ZoneId = plant.ZoneId,
                MomId = plant.MomId,
                Description = plant.Description,
                IsActive = plant.IsActive,
                CreatedAt = plant.CreatedAt
            };
        }

        public async Task<PlantDto> CreateAsync(PlantWriteDto dto)
        {
            var specimen = await _specimenService.GetByIdAsync(dto.SpecimenId);
            if (specimen == null)
            {
                _logger.LogError("Specimen with ID {SpecimenId} not found. Cannot create plant.", dto.SpecimenId);
                throw new ArgumentException($"Specimen with ID {dto.SpecimenId} does not exist.");
            }

            var zone = await _zoneService.GetByIdAsync(dto.ZoneId);
            if (zone == null)
            {
                _logger.LogError("Zone with ID {ZoneId} not found. Cannot create plant.", dto.ZoneId);
                throw new ArgumentException($"Zone with ID {dto.ZoneId} does not exist.");
            }

            if(dto.MomId.HasValue)
            {
                var momPlant = await GetByIdAsync(dto.MomId.Value);
                if (momPlant == null)
                {
                    _logger.LogError("Mom Plant with ID {MomId} not found. Cannot create plant.", dto.MomId.Value);
                    throw new ArgumentException($"Mom Plant with ID {dto.MomId.Value} does not exist.");
                }
            }

            var plant = new Plant
            {
                AcquiredDate = dto.AcquiredDate,
                SpecimenId = dto.SpecimenId,
                ZoneId = dto.ZoneId,
                MomId = dto.MomId,
                Description = dto.Description,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(plant);
            _logger.LogInformation("New plant created with ID {PlantId}.", plant.Id);

            return new PlantDto
            {
                Id = plant.Id,
                AcquiredDate = plant.AcquiredDate,
                SpecimenId = plant.SpecimenId,
                ZoneId = plant.ZoneId,
                MomId = plant.MomId,
                Description = plant.Description,
                IsActive = plant.IsActive,
                CreatedAt = plant.CreatedAt
            };
        }

        public async Task<PlantDto> UpdateAsync(int id, PlantWriteDto dto)
        {
            var plant = await _repository.GetByIdAsync(id);
            if (plant == null)
            {
                _logger.LogError("Plant with ID {PlantId} not found. Cannot update.", id);
                throw new ArgumentException($"Plant with ID {id} does not exist.");
            }

            var specimen = await _specimenService.GetByIdAsync(dto.SpecimenId);
            if (specimen == null)
            {
                _logger.LogError("Specimen with ID {SpecimenId} not found. Cannot update plant.", dto.SpecimenId);
                throw new ArgumentException($"Specimen with ID {dto.SpecimenId} does not exist.");
            }

            var zone = await _zoneService.GetByIdAsync(dto.ZoneId);
            if (zone == null)
            {
                _logger.LogError("Zone with ID {ZoneId} not found. Cannot update plant.", dto.ZoneId);
                throw new ArgumentException($"Zone with ID {dto.ZoneId} does not exist.");
            }

            if(dto.MomId.HasValue)
            {
                var momPlant = await GetByIdAsync(dto.MomId.Value); 
                if (momPlant == null)
                {
                    _logger.LogError("Mom Plant with ID {MomId} not found. Cannot update plant.", dto.MomId.Value);
                    throw new ArgumentException($"Mom Plant with ID {dto.MomId.Value} does not exist.");
                }
            }

            plant.AcquiredDate = dto.AcquiredDate;
            plant.SpecimenId = dto.SpecimenId;
            plant.ZoneId = dto.ZoneId;
            plant.MomId = dto.MomId;
            plant.Description = dto.Description;

            await _repository.SaveAsync();
            _logger.LogInformation("Plant with ID {PlantId} updated successfully.", id);

            return new PlantDto
            {
                Id = plant.Id,
                AcquiredDate = plant.AcquiredDate,
                SpecimenId = plant.SpecimenId,
                ZoneId = plant.ZoneId,
                MomId = plant.MomId,
                Description = plant.Description,
                IsActive = plant.IsActive,
                CreatedAt = plant.CreatedAt
            };
        }

        public async Task DeleteAsync(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
            {
                _logger.LogError("Plant with ID {PlantId} not found. Cannot delete.", id);
                throw new KeyNotFoundException($"Plant with ID {id} not found.");
            }

            _logger.LogInformation("Plant with ID {PlantId} deleted successfully.", id);
        }
    }
}
