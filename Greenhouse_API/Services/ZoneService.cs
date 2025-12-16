using Greenhouse_API.DTOs;
using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Greenhouse_API.Services
{
    public class ZoneService : IZoneService
    {
        private SerreContext _context;
        private readonly ILogger<ZoneService> _logger;
        private readonly IRepository<ZoneCategory, int> _zoneCategoryService;

        public ZoneService(SerreContext context, ILogger<ZoneService> logger, IRepository<ZoneCategory, int> zoneCategoryService)
        {
            _context = context;
            _logger = logger;
            _zoneCategoryService = zoneCategoryService;
        }

        public Task<IEnumerable<ZoneDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ZoneDto?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ZoneDto> CreateAsync(ZoneWriteDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<ZoneDto> UpdateAsync(int id, ZoneWriteDto dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
