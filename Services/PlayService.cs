using Data.Theatrical;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Services.Common;
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
        public async Task<ServiceResponse<Play>> GetPlayById(int id)
        {
            var play = await _db.Plays
                .FirstOrDefaultAsync(i => i.Id == id);

            if (play == null)
            {
                return new ServiceResponse<Play>("Play not found");
            }

            return new ServiceResponse<Play>(play);
        }

        public async Task<ServiceResponse<List<Play>>> GetPlays()
        {
            var play = await _db
                .Plays.ToListAsync();

            return new ServiceResponse<List<Play>>(play);
        }

        public async Task<ServiceResponse<Play>> CreatePlay(Play play)
        {
            var existingPlay = await _db.Plays
                .FirstOrDefaultAsync(i => i.Id == play.Id);

            if (existingPlay != null)
            {
                return new ServiceResponse<Play>("Play already exists");
            }

            _db.Plays.Add(play);
            await _db.SaveChangesAsync();
            return new ServiceResponse<Play>(play);
        }

        public async Task<ServiceResponse<Play>> PutPlay(Play play)
        {
            var existingPlay = await _db.Plays.FindAsync(play.Id);

            if (existingPlay == null)
            {
                return new ServiceResponse<Play>("Play not found");
            }
            _db.Entry(existingPlay).CurrentValues.SetValues(play);

            await _db.SaveChangesAsync();

            return new ServiceResponse<Play>(existingPlay);
        }

        public async Task<ServiceResponse<bool>> DeletePlay(int id)
        {
            var deletePlay = await _db.Plays.FirstOrDefaultAsync(i => i.Id == id);
            if (deletePlay != null)
            {
                _db.Plays.Remove(deletePlay);
                await _db.SaveChangesAsync();
                return new ServiceResponse<bool>(true);
            }

            return new ServiceResponse<bool>("Play not found");
        }

    }
}