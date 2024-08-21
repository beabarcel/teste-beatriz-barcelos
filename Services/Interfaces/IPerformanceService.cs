using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IPerformanceService
    {
        Task<List<Performance>> GetPerformances();
        Task<Performance> GetPerformanceById(int id);
        Task<Performance> CreatePerformance(Performance performance);
        Task<Performance> PutPerformance(Performance performance);
        Task<bool> DeletePerformance(int id);
    }
}
