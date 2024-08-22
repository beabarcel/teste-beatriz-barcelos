using Domain.Models;
using Services.Common;

namespace Services.Interfaces
{
    public interface IPerformanceService
    {
        Task<ServiceResponse<List<Performance>>> GetPerformances();
        Task<ServiceResponse<Performance>> GetPerformanceById(int id);
        Task<ServiceResponse<Performance>> CreatePerformance(Performance performance);
        Task<ServiceResponse<Performance>> PutPerformance(Performance performance);
        Task<ServiceResponse<bool>> DeletePerformance(int id);
    }
}
