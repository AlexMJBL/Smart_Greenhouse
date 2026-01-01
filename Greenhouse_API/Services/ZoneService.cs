using Greenhouse_API.DTOs;
using Greenhouse_API.Exceptions;
using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Greenhouse_API.Services
{
    public class ZoneService : IZoneService
    {
        private IRepository<Zone> _repository;
        private readonly ILogger<ZoneService> _logger;
        private readonly IZoneCategoryService _zoneCategoryService;

        public ZoneService(IRepository<Zone> repository, ILogger<ZoneService> logger, IZoneCategoryService zoneCategoryService)
        {
            _repository = repository;
            _logger = logger;
            _zoneCategoryService = zoneCategoryService;
        }

        public async Task<IEnumerable<ZoneDto>> GetAllAsync()
        {
            _logger.LogInformation("Retrieving all zones");
            var zones = await _repository.GetAllAsync();
            return zones.Select(z => new ZoneDto
            {
                Id = z.Id,
                Description = z.Description,
                ZoneCategoryId = z.ZoneCategoryId,
                CreatedAt = z.CreatedAt
            }); 
        }

        public async Task<ZoneDto?> GetByIdAsync(int id)
        {
            var zone = await _repository.GetByIdAsync(id);
            if (zone == null)
            {
                _logger.LogWarning("Zone with ID: {ZoneId} not found", id);
                return null;
            }

            _logger.LogInformation("Zone with ID: {ZoneId} retrieved", id);
            return new ZoneDto
            {
                Id = zone.Id,
                Description = zone.Description,
                ZoneCategoryId = zone.ZoneCategoryId,
                CreatedAt = zone.CreatedAt
            };
        }

        public async Task<ZoneDto> CreateAsync(ZoneWriteDto dto)
        {
            var zoneCategory = await _zoneCategoryService.GetByIdAsync(dto.ZoneCategoryId);
            if (zoneCategory == null)
            {
                _logger.LogWarning("ZoneCategory with ID: {ZoneCategoryId} not found for zone creation", dto.ZoneCategoryId);
                throw new NotFoundException("ZoneCategory not found");
            }

            var zone = new Zone
            {
                Description = dto.Description,
                ZoneCategoryId = dto.ZoneCategoryId,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(zone);
            _logger.LogInformation("Zone created with ID: {ZoneId}", zone.Id);

            return new ZoneDto
            {
                Id = zone.Id,
                Description = zone.Description,
                ZoneCategoryId = zone.ZoneCategoryId,
                CreatedAt = zone.CreatedAt
            };
        }

        public async Task<ZoneDto> UpdateAsync(int id, ZoneWriteDto dto)
        {

            var zone = await _repository.GetByIdAsync(id);
            if (zone == null)
            {
                _logger.LogError("Zone with ID {ZoneId} not found. Cannot update.", id);
                throw new NotFoundException($"Zone with ID {id} does not exist.");
            }

            var zoneCategory = await _zoneCategoryService.GetByIdAsync(dto.ZoneCategoryId);
            if (zoneCategory == null)
            {
                _logger.LogWarning("ZoneCategory with ID: {ZoneCategoryId} not found for zone creation", dto.ZoneCategoryId);
                throw new NotFoundException("ZoneCategory not found");
            }

            var zoneDto = new Zone
            {
                Description = dto.Description,
                ZoneCategoryId = dto.ZoneCategoryId,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(zoneDto);
            _logger.LogInformation("Zone created with ID: {ZoneId}", zoneDto.Id);

            return new ZoneDto
            {
                Id = zone.Id,
                Description = zone.Description,
                ZoneCategoryId = zone.ZoneCategoryId,
                CreatedAt = zone.CreatedAt
            };
        }

        public async Task DeleteAsync(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
            {
                _logger.LogWarning("Zone with ID {ZoneId} not found for deletion", id);
                throw new NotFoundException("Zone not found");
            }
            _logger.LogInformation("Zone with ID {ZoneId} deleted", id);
        }
    }
}
