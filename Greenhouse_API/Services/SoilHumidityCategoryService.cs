using Greenhouse_API.DTOs;
using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Greenhouse_API.Services
{
    public class SoilHumidityCategoryService : ISoilHumidityCategoryService
    {
        private SerreContext _context;
        private readonly ILogger<SoilHumidityCategoryService> _logger;
        public SoilHumidityCategoryService(SerreContext context, ILogger<SoilHumidityCategoryService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Task<IEnumerable<SoilHumidityCategoryDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SoilHumidityCategoryDto?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<SoilHumidityCategoryDto> CreateAsync(SoilHumidityCategoryWriteDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<SoilHumidityCategoryDto> UpdateAsync(int id, SoilHumidityCategoryWriteDto dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
