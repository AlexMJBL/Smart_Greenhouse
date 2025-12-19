using Greenhouse_API.Data;
using Greenhouse_API.DTOs;
using Greenhouse_API.Exceptions;
using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Greenhouse_API.Services
{
    public class FertilizerService : IFertilizerService
    {
        private readonly IRepository<Fertilizer> _repository;
        private readonly ILogger<FertilizerService> _logger;
        private readonly IPlantService _plantService;
        public FertilizerService(IRepository<Fertilizer> repository, ILogger<FertilizerService> logger, IPlantService plantService)
        {
            _repository = repository;
            _logger = logger;
            _plantService = plantService;
        }

        public async Task<FertilizerDto> CreateAsync(FertilizerWriteDto dto)
        {
            var plant = await _plantService.GetByIdAsync(dto.PlantId);
            if(plant == null)
            {
                _logger.LogWarning("Plant with ID: {PlantId} not found for fertilizer creation", dto.PlantId);
                throw new NotFoundException("Plant not found");
            }

            var fertilizer = new Fertilizer
            {
                Type = dto.Type,
                PlantId = dto.PlantId,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(fertilizer);
            _logger.LogInformation("Fertilizer created with ID: {FertilizerId}", fertilizer.Id);

            return new FertilizerDto
            {
                Id = fertilizer.Id,
                Type = fertilizer.Type,
                PlantId = fertilizer.PlantId,
                CreatedAt = fertilizer.CreatedAt
            };
        }

        public async Task DeleteAsync(int id)
        {
            var deleted = await _repository.DeleteAsync(id);

            if (!deleted)
            {
                _logger.LogWarning("Fertilizer with ID {FertilizerId} not found for deletion", id);
                throw new NotFoundException("Fertilizer not found");
            }

            _logger.LogInformation("Fertilizer with ID {FertilizerId} deleted", id);
        }

        public async Task<IEnumerable<FertilizerDto>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all fertilizers");
            var fertilizers = await _repository.GetAllAsync();

            return fertilizers.Select(f => new FertilizerDto
            {
                Id = f.Id,
                Type = f.Type,
                PlantId = f.PlantId,
                CreatedAt = f.CreatedAt
            });
        }

        public async Task<FertilizerDto?> GetByIdAsync(int id)
        {
            var fertilizer = await _repository.GetByIdAsync(id);
            if (fertilizer == null)
            {
                _logger.LogWarning("Fertilizer with ID: {FertilizerId} not found", id);
                return null;
            }

            _logger.LogInformation("Fertilizer with ID: {FertilizerId} retrieved", id);
            return new FertilizerDto
            {
                Id = fertilizer.Id,
                Type = fertilizer.Type,
                PlantId = fertilizer.PlantId,
                CreatedAt = fertilizer.CreatedAt
            };
        }

        public async Task<FertilizerDto> UpdateAsync(int id, FertilizerWriteDto dto)
        {
            var fertilizer = await _repository.GetByIdAsync(id);
            if (fertilizer == null)
            {
                _logger.LogWarning("Fertilizer with ID: {FertilizerId} not found for update", id);
                throw new NotFoundException("Fertilizer not found");
            }

            var plant = await _plantService.GetByIdAsync(dto.PlantId);
            if (plant == null)
            {
                _logger.LogWarning(
                    "Plant with ID {PlantId} not found for fertilizer update", dto.PlantId);
                throw new NotFoundException("Plant not found");
            }

            fertilizer.Type = dto.Type;
            fertilizer.PlantId = dto.PlantId;

            await _repository.SaveAsync();
            _logger.LogInformation("Fertilizer with ID: {FertilizerId} updated", id);

            return new FertilizerDto
            {
                Id = fertilizer.Id,
                Type = fertilizer.Type,
                PlantId = fertilizer.PlantId,
                CreatedAt = fertilizer.CreatedAt
            };
        }
    }
}
