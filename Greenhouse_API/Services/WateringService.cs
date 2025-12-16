using Greenhouse_API.DTOs;
using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Greenhouse_API.Services
{
    public class WateringService : IWateringService
    {
        private SerreContext _context;
        private readonly ILogger<WateringService> _logger;

        private readonly IRepository<Plant, int> _plantService;

        public WateringService(SerreContext context, ILogger<WateringService> logger, IRepository<Plant, int> plantService)
        {
            _context = context;
            _logger = logger;
            _plantService = plantService;   
        }

        public Task<IEnumerable<WateringDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<WateringDto?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<WateringDto> CreateAsync(WateringWriteDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<WateringDto> UpdateAsync(int id, WateringWriteDto dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
