using Greenhouse_API.DTOs;
using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Greenhouse_API.Services
{
    public class ZoneCategoryService : IZoneCategoryService
    {
        private SerreContext _context;
        private readonly ILogger<ZoneCategoryService> _logger;
        public ZoneCategoryService(SerreContext context, ILogger<ZoneCategoryService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Task<IEnumerable<ZoneCategoryDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ZoneCategoryDto?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ZoneCategoryDto> CreateAsync(ZoneCategoryWriteDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<ZoneCategoryDto> UpdateAsync(int id, ZoneCategoryWriteDto dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
