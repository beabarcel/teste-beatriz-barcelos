using Domain.Models;

namespace Services.Interfaces
{
    public interface IPlayService
    {
        Task<List<Play>> GetPlays();
        Task<Play> GetPlayById(int id);
        Task<Play> CreatePlay(Play play);
        Task<Play> PutPlay(Play play);
        Task<bool> DeletePlay(int id);
    }
}
