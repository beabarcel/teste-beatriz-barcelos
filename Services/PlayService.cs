using Data.Theatrical;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace Services
{
    public class PlayService : IPlayService
    {

        private readonly TheatricalContext _db;
        public PlayService(TheatricalContext db)
        {
            _db = db;
        }
        public async Task<Play> GetPlayById(int id)
        {
            return await _db.Plays.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<Play>> GetPlays()
        {
            return await _db.Plays.ToListAsync();
        }

        public async Task<Play> CreatePlay(Play play)
        {
            _db.Plays.Add(play);
            await _db.SaveChangesAsync();
            return play;
        }

        public async Task<Play> PutPlay(Play play)
        {
            if (play == null)
            {
                return null;
            }

            _db.Plays.Update(play);

            await _db.SaveChangesAsync();

            return play;
        }
        public async Task<bool> DeletePlay(int id)
        {
            var deletePlay = await _db.Plays.FirstOrDefaultAsync(i => i.Id == id);
            if (deletePlay != null)
            {
                _db.Plays.Remove(deletePlay);
                await _db.SaveChangesAsync();
                return true;
            }

            return false;
        }

    }
}