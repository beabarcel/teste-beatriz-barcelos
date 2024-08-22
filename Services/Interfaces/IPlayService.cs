using Domain.Models;
using Services.Common;

namespace Services.Interfaces
{
    public interface IPlayService
    {
        Task<ServiceResponse<List<Play>>> GetPlays();
        Task<ServiceResponse<Play>> GetPlayById(int id);
        Task<ServiceResponse<Play>> CreatePlay(Play play);
        Task<ServiceResponse<Play>> PutPlay(Play play);
        Task<ServiceResponse<bool>> DeletePlay(int id);
    }
}
