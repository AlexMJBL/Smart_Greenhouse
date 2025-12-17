using Greenhouse_API.DTOs;
using Greenhouse_API.Exceptions;
using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Greenhouse_API.Services
{
    public class SoilHumidityCategoryService : ISoilHumidityCategoryService
    {
        private IRepository<SoilHumidityCategory> _repository;
        private readonly ILogger<SoilHumidityCategoryService> _logger;
        public SoilHumidityCategoryService(IRepository<SoilHumidityCategory> repository, ILogger<SoilHumidityCategoryService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<SoilHumidityCategoryDto>> GetAllAsync()
        {
            _logger.LogInformation("Retrieving all soil humidity categories");
            var SoilHumidityCategorys = await _repository.GetAllAsync();

            return SoilHumidityCategorys.Select(soilHumidityCategory => new SoilHumidityCategoryDto
            {
                Id = soilHumidityCategory.Id,
                Name = soilHumidityCategory.Name,
                MinHumidityPct = soilHumidityCategory.MinHumidityPct,
                MaxHumidityPct = soilHumidityCategory.MaxHumidityPct,
                CreatedAt = soilHumidityCategory.CreatedAt
            });
        }

        public async Task<SoilHumidityCategoryDto?> GetByIdAsync(int id)
        {
            var soilHumidityCategory = await _repository.GetByIdAsync(id);
            if (soilHumidityCategory == null)
            {
                _logger.LogWarning("SoilHumidityCategory with ID: {SoilHumidityCategoryId} not found", id);
                return null;
            }

            _logger.LogInformation("SoilHumidityCategory with ID: {SoilHumidityCategoryId} retrieved", id);
            return new SoilHumidityCategoryDto
            {
                Id = soilHumidityCategory.Id,
                Name = soilHumidityCategory.Name,
                MinHumidityPct = soilHumidityCategory.MinHumidityPct,
                MaxHumidityPct = soilHumidityCategory.MaxHumidityPct,
                CreatedAt = soilHumidityCategory.CreatedAt
            };
        }

        public async Task<SoilHumidityCategoryDto> CreateAsync(SoilHumidityCategoryWriteDto dto)
        {
            var soilHumidityCategory = new SoilHumidityCategory
            {
                Name = dto.Name,
                MinHumidityPct = dto.MinHumidityPct,
                MaxHumidityPct = dto.MaxHumidityPct,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(soilHumidityCategory);
            _logger.LogInformation("SoilHumidityCategory created with ID: {SoilHumidityCategoryId}", soilHumidityCategory.Id);

            return new SoilHumidityCategoryDto
            {
                Id = soilHumidityCategory.Id,
                Name = soilHumidityCategory.Name,
                MinHumidityPct = soilHumidityCategory.MinHumidityPct,
                MaxHumidityPct = soilHumidityCategory.MaxHumidityPct,
                CreatedAt = soilHumidityCategory.CreatedAt
            };
        }

        public async Task<SoilHumidityCategoryDto> UpdateAsync(int id, SoilHumidityCategoryWriteDto dto)
        {
            var soilHumidityCategory = await _repository.GetByIdAsync(id);
            if (soilHumidityCategory == null)
            {
                _logger.LogWarning("SoilHumidityCategory with ID: {SoilHumidityCategoryId} not found for update", id);
                throw new NotFoundException("SoilHumidityCategory not found");
            }

            soilHumidityCategory.Name = dto.Name;
            soilHumidityCategory.MinHumidityPct = dto.MinHumidityPct;
            soilHumidityCategory.MaxHumidityPct = dto.MaxHumidityPct;

            await _repository.SaveAsync();
            _logger.LogInformation("SoilHumidityCategory with ID: {SoilHumidityCategoryId} updated", id);

            return new SoilHumidityCategoryDto
            {
                Id = soilHumidityCategory.Id,
                Name = soilHumidityCategory.Name,
                MinHumidityPct = soilHumidityCategory.MinHumidityPct,
                MaxHumidityPct = soilHumidityCategory.MaxHumidityPct,
                CreatedAt = soilHumidityCategory.CreatedAt
            };
        }
        public async Task DeleteAsync(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
            {
                _logger.LogWarning("SoilHumidityCategory with ID {SoilHumidityCategoryId} not found for deletion", id);
                throw new NotFoundException("SoilHumidityCategory not found");
            }
            _logger.LogInformation("SoilHumidityCategory with ID {SoilHumidityCategoryId} deleted", id);
        }
    }
}
