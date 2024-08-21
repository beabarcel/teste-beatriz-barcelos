using Data.Theatrical;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace Services
{
    public class PerformanceService : IPerformanceService
    {

        private readonly TheatricalContext _db;
        public PerformanceService(TheatricalContext db)
        {
            _db = db;
        }
        public async Task<Performance> GetPerformanceById(int id)
        {
            return await _db.Performances.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<Performance>> GetPerformances()
        {
            return await _db.Performances.ToListAsync();
        }

        public async Task<Performance> CreatePerformance(Performance performance)
        {
            _db.Performances.Add(performance);
            await _db.SaveChangesAsync();
            return performance;
        }

        public async Task<Performance> PutPerformance(Performance performance)
        {
            if (performance == null)
            {
                return null;
            }

            _db.Performances.Update(performance);

            await _db.SaveChangesAsync();

            return performance;
        }
        public async Task<bool> DeletePerformance(int id)
        {
            var deletePerformance = await _db.Performances.FirstOrDefaultAsync(i => i.Id == id);
            if (deletePerformance != null)
            {
                _db.Performances.Remove(deletePerformance);
                await _db.SaveChangesAsync();
                return true;
            }

            return false;
        }

    }
}
