using Greenhouse_API.DTOs;
using Greenhouse_API.Exceptions;
using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Greenhouse_API.Services
{
    public class SpecimenService : ISpecimenService
    {
        private IRepository<Specimen> _repository;
        private readonly ILogger<SpecimenService> _logger;
        private readonly ISoilHumidityCategoryService _soilHumidityCategoryService;
        
        public SpecimenService(IRepository<Specimen> repository, ILogger<SpecimenService> logger, ISoilHumidityCategoryService soilHumidityCategoryService)
        {
            _repository = repository;
            _logger = logger;
            _soilHumidityCategoryService = soilHumidityCategoryService;
        }

        public async Task<IEnumerable<SpecimenDto>> GetAllAsync()
        {
            _logger.LogInformation("Retrieving all specimens");
            var specimens = await _repository.GetAllAsync();
            return specimens.Select(specimen => new SpecimenDto
            {
                Id = specimen.Id,
                Name = specimen.Name,
                SoilHumidityCatId = specimen.SoilHumidityCatId,
                CreatedAt = specimen.CreatedAt
            });
        }

        public async Task<SpecimenDto?> GetByIdAsync(int id)
        {
            var specimen =  await _repository.GetByIdAsync(id);
            if (specimen == null)
            {
                _logger.LogWarning("Specimen with ID: {SpecimenId} not found", id);
                return null;
            }

            _logger.LogInformation("Retrieving specimen with ID: {SpecimenId}", id);
            return new SpecimenDto
            {
                Id = specimen.Id,
                Name = specimen.Name,
                SoilHumidityCatId = specimen.SoilHumidityCatId,
                CreatedAt = specimen.CreatedAt
            };
        }

        public async Task<SpecimenDto> CreateAsync(SpecimenWriteDto dto)
        {
            var SoilHumidityCategory = await _soilHumidityCategoryService.GetByIdAsync(dto.SoilHumidityCatId);

            if (SoilHumidityCategory == null)
            {
                _logger.LogWarning("Soil Humidity Category with ID: {SoilHumidityCatId} not found for specimen creation", dto.SoilHumidityCatId);
                throw new NotFoundException("Soil Humidity Category not found");
            }

            var specimen = new Specimen
            {
                Name = dto.Name,
                SoilHumidityCatId = dto.SoilHumidityCatId,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(specimen);
            _logger.LogInformation("Specimen created with ID: {SpecimenId}", specimen.Id);

            return new SpecimenDto
            {
                Id = specimen.Id,
                Name = specimen.Name,
                SoilHumidityCatId = specimen.SoilHumidityCatId,
                CreatedAt = specimen.CreatedAt
            };
        }

        public async Task<SpecimenDto> UpdateAsync(int id, SpecimenWriteDto dto)
        {
            var specimen = await _repository.GetByIdAsync(id);
            if (specimen == null)
            {
                _logger.LogWarning("Specimen with ID: {SpecimenId} not found for update", id);
                throw new NotFoundException("Specimen not found");
            }

            var SoilHumidityCategory = await _soilHumidityCategoryService.GetByIdAsync(dto.SoilHumidityCatId);
            if (SoilHumidityCategory == null)
            {
                _logger.LogWarning("Soil Humidity Category with ID: {SoilHumidityCatId} not found for specimen update", dto.SoilHumidityCatId);
                throw new NotFoundException("Soil Humidity Category not found");
            }

            specimen.Name = dto.Name;
            specimen.SoilHumidityCatId = dto.SoilHumidityCatId;

            await _repository.SaveAsync();
            _logger.LogInformation("Specimen with ID: {SpecimenId} updated", id);

            return new SpecimenDto
            {
                Id = specimen.Id,
                Name = specimen.Name,
                SoilHumidityCatId = specimen.SoilHumidityCatId,
                CreatedAt = specimen.CreatedAt
            };
        }

        public async Task DeleteAsync(int id)
        {
            var delete = await _repository.DeleteAsync(id);
            if (!delete)
            {
                _logger.LogWarning("Specimen with ID: {SpecimenId} not found for deletion", id);
                throw new NotFoundException("Specimen not found");
            }
            _logger.LogInformation("Specimen with ID: {SpecimenId} deleted", id);
        }
    }
}
