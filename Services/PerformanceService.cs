using Data.Theatrical;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Services.Common;
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
        public async Task<ServiceResponse<Performance>> GetPerformanceById(int id)
        {
            var performance = await _db.Performances
                .FirstOrDefaultAsync(i => i.Id == id);

            if (performance == null)
            {
                return new ServiceResponse<Performance>("Performance not found");
            }

            return new ServiceResponse<Performance>(performance);
        }

        public async Task<ServiceResponse<List<Performance>>> GetPerformances()
        {
            var performance = await _db
                .Performances.ToListAsync();

            return new ServiceResponse<List<Performance>>(performance);
        }

        public async Task<ServiceResponse<Performance>> CreatePerformance(Performance performance)
        {
            var existingPerformance = await _db.Performances
                .FirstOrDefaultAsync(i => i.Id == performance.Id);

            if (existingPerformance != null)
            {
                return new ServiceResponse<Performance>("Performance already exists");
            }

            _db.Performances.Add(performance);
            await _db.SaveChangesAsync();
            return new ServiceResponse<Performance>(performance);
        }

        public async Task<ServiceResponse<Performance>> PutPerformance(Performance performance)
        {
            var existingPerformance = await _db.Performances.FindAsync(performance.Id);

            if (existingPerformance == null)
            {
                return new ServiceResponse<Performance>("Performance not found");
            }
            _db.Entry(existingPerformance).CurrentValues.SetValues(performance);

            await _db.SaveChangesAsync();

            return new ServiceResponse<Performance>(existingPerformance);
        }


        public async Task<ServiceResponse<bool>> DeletePerformance(int id)
        {
            var deletePerformance = await _db.Performances.FirstOrDefaultAsync(i => i.Id == id);
            if (deletePerformance != null)
            {
                _db.Performances.Remove(deletePerformance);
                await _db.SaveChangesAsync();
                return new ServiceResponse<bool>(true);
            }

            return new ServiceResponse<bool>("Performance not found");
        }
    }
}