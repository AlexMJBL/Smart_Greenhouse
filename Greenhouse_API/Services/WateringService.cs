using Greenhouse_API.DTOs;
using Greenhouse_API.Exceptions;
using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Greenhouse_API.Services
{
    public class WateringService : IWateringService
    {
        private IRepository<Watering> _repository;  
        private readonly ILogger<WateringService> _logger;

        private readonly IPlantService _plantService;

        public WateringService(IRepository<Watering> repository, ILogger<WateringService> logger, IPlantService plantService)
        {
            _repository = repository;
            _logger = logger;
            _plantService = plantService;   
        }

        public async Task<IEnumerable<WateringDto>> GetAllAsync()
        {
            _logger.LogInformation("Retrieving all waterings");
            var waterings = await _repository.GetAllAsync();

            return waterings.Select(w => new WateringDto
            {
                Id = w.Id,
                HumPctBefore = w.HumPctBefore,
                HumPctAfter = w.HumPctAfter,
                WaterQuantityMl = w.WaterQuantityMl,
                PlantId = w.PlantId,
                CreatedAt = w.CreatedAt
            });
        }

        public async Task<WateringDto?> GetByIdAsync(int id)
        {
            var watering = await _repository.GetByIdAsync(id);
            if (watering == null)
            {
                _logger.LogWarning("Watering with ID: {WateringId} not found", id);
                return null;
            }

            _logger.LogInformation("Watering with ID: {WateringId} retrieved", id);
            return new WateringDto
            {
                Id = watering.Id,
                HumPctBefore = watering.HumPctBefore,
                HumPctAfter = watering.HumPctAfter,
                WaterQuantityMl = watering.WaterQuantityMl,
                PlantId = watering.PlantId,
                CreatedAt = watering.CreatedAt
            };
        }

        public async Task<WateringDto> CreateAsync(WateringWriteDto dto)
        {
            var plant = await _plantService.GetByIdAsync(dto.PlantId);
            if (plant == null)
            {
                _logger.LogWarning("Plant with ID: {PlantId} not found for watering creation", dto.PlantId);
                throw new NotFoundException("Plant not found");
            }

            var watering = new Watering
            {
                HumPctBefore = dto.HumPctBefore,
                HumPctAfter = dto.HumPctAfter,
                WaterQuantityMl = dto.WaterQuantityMl,
                PlantId = dto.PlantId,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(watering);
            _logger.LogInformation("Watering created with ID: {WateringId}", watering.Id);

            return new WateringDto
            {
                Id = watering.Id,
                HumPctBefore = watering.HumPctBefore,
                HumPctAfter = watering.HumPctAfter,
                WaterQuantityMl = watering.WaterQuantityMl,
                PlantId = watering.PlantId,
                CreatedAt = watering.CreatedAt
            };
        }

        public async Task<WateringDto> UpdateAsync(int id, WateringWriteDto dto)
        {
            var watering = await _repository.GetByIdAsync(id);
            if (watering == null)
            {
                _logger.LogWarning("Watering with ID: {WateringId} not found for update", id);
                throw new NotFoundException("Watering not found");
            }

            var plant = await _plantService.GetByIdAsync(dto.PlantId);
            if (plant == null)
            {
                _logger.LogWarning("Plant with ID {PlantId} not found for watering update", dto.PlantId);
                throw new NotFoundException("Plant not found");
            }

            watering.HumPctBefore = dto.HumPctBefore;
            watering.HumPctAfter = dto.HumPctAfter;
            watering.WaterQuantityMl = dto.WaterQuantityMl;
            watering.PlantId = dto.PlantId;

            await _repository.SaveAsync();
            _logger.LogInformation("Watering with ID: {WateringId} updated", id);

            return new WateringDto
            {
                Id = watering.Id,
                HumPctBefore = watering.HumPctBefore,
                HumPctAfter = watering.HumPctAfter,
                WaterQuantityMl = watering.WaterQuantityMl,
                PlantId = watering.PlantId,
                CreatedAt = watering.CreatedAt
            };
        }

        public async Task DeleteAsync(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
            {
                _logger.LogWarning("Watering with ID {WateringId} not found for deletion", id);
                throw new NotFoundException("Watering not found");
            }

            _logger.LogInformation("Watering with ID {WateringId} deleted", id);
        }
    }
}
