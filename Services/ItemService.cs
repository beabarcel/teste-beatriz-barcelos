using Data.Theatrical;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace Services
{
    public class ItemService : IItemService
    {
        private readonly TheatricalContext _db;
        public ItemService(TheatricalContext db)
        {
            _db = db;
        }

        public async Task<Item> GetItemById(int id)
        {
            return await _db.Items.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<Item>> GetItems()
        {
            return await _db.Items.ToListAsync();
        }

        public async Task<Item> CreateItem(Item item)
        {
            _db.Items.Add(item);
            await _db.SaveChangesAsync();
            return item;
        }

        public async Task<Item> PutItem(Item item)
        {
            if (item == null)
            {
                return null;
            }

            _db.Items.Update(item);

            await _db.SaveChangesAsync();

            return item;
        }
        public async Task<bool> DeleteItem(int id)
        {
            var deleteItem = await _db.Items.FirstOrDefaultAsync(i => i.Id == id);
            if (deleteItem != null)
            {
                _db.Items.Remove(deleteItem);
                await _db.SaveChangesAsync();
                return true;
            }

            return false;
        }

    }
}
