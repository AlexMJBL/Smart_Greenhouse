using Greenhouse_API.DTOs;
using Greenhouse_API.Exceptions;
using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Greenhouse_API.Services
{
    public class ObservationService : IObservationService
    {
        private readonly IRepository<Observation> _repository;
        private readonly ILogger<ObservationService> _logger;

        private readonly IPlantService _plantService;

        public ObservationService(IRepository<Observation> repository, ILogger<ObservationService> logger, IPlantService plantService)
        {
            _repository = repository;
            _logger = logger;
            _plantService = plantService;
        }

        public async Task<ObservationDto> CreateAsync(ObservationWriteDto dto)
        {
            var plant = await  _plantService.GetByIdAsync(dto.PlantId);
            if (plant == null)
            {
                _logger.LogWarning("Attempted to create observation for non-existent plant with ID {PlantId}", dto.PlantId);
                throw new NotFoundException($"Plant with ID {dto.PlantId} does not exist.");
            }

            var observation = new Observation
            {
                PlantId = dto.PlantId,
                Rating = dto.Rating,
                Comments = dto.Comments,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(observation);
            _logger.LogInformation("Created new observation with ID {ObservationId} for plant ID {PlantId}", observation.Id, dto.PlantId);

            return new ObservationDto
            {
                Id = observation.Id,
                PlantId = observation.PlantId,
                Rating = observation.Rating,
                Comments = observation.Comments,
                CreatedAt = observation.CreatedAt
            };
        }

        public async Task DeleteAsync(int id)
        {
            var deleted = await _repository.DeleteAsync(id);

            if(!deleted)
            {
                _logger.LogWarning("Attempted to delete non-existent observation with ID {ObservationId}", id);
                throw new NotFoundException($"Observation with ID {id} not found.");
            }

            _logger.LogInformation("Deleted observation with ID {ObservationId}", id);
        }

        public async Task<IEnumerable<ObservationDto>> GetAllAsync()
        {
           _logger.LogInformation("Fetching all observations");
            var observations = await _repository.GetAllAsync(); 

            return observations.Select(o => new ObservationDto
            {
                Id = o.Id,
                PlantId = o.PlantId,
                Rating = o.Rating,
                Comments = o.Comments,
                CreatedAt = o.CreatedAt
            });
        }

        public async Task<ObservationDto?> GetByIdAsync(int id)
        {
            var observation = await _repository.GetByIdAsync(id);
            if(observation == null)
            {
                _logger.LogWarning("Observation with ID {ObservationId} not found", id);
                return null;
            }   

            _logger.LogInformation("Fetched observation with ID {ObservationId}", id);
            return new ObservationDto
            {
                Id = observation.Id,
                PlantId = observation.PlantId,
                Rating = observation.Rating,
                Comments = observation.Comments,
                CreatedAt = observation.CreatedAt
            };
        }

        public async Task<ObservationDto> UpdateAsync(int id, ObservationWriteDto dto)
        {
            var observation = await _repository.GetByIdAsync(id);
            if (observation == null)
            {
                _logger.LogWarning("Observation with ID {ObservationId} not found for update", id);
                throw new NotFoundException("Observation not found");
            }

            var plant = await _plantService.GetByIdAsync(dto.PlantId);
            if (plant == null)
            {
                _logger.LogWarning("Attempted to update observation for non-existent plant with ID {PlantId}", dto.PlantId);
                throw new NotFoundException($"Plant with ID {dto.PlantId} does not exist.");
            }

            observation.PlantId = dto.PlantId;
            observation.Rating = dto.Rating;
            observation.Comments = dto.Comments;

            await _repository.SaveAsync();
            _logger.LogInformation("Updated observation with ID {ObservationId}", id);

            return new ObservationDto
            {
                Id = observation.Id,
                PlantId = observation.PlantId,
                Rating = observation.Rating,
                Comments = observation.Comments,
                CreatedAt = observation.CreatedAt
            };
        }
    }
}
