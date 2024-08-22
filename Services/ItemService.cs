using Data.Theatrical;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Services.Common;
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

        public async Task<ServiceResponse<Item>> GetItemById(int id)
        {
            var item = await _db.Items
                .FirstOrDefaultAsync(i => i.Id == id);
            
            if (item == null)
            {
                return new ServiceResponse<Item>("Item not found");
            }
        
            return new ServiceResponse<Item>(item);
        }

        public async Task<ServiceResponse<List<Item>>> GetItems()
        {
            var items = await _db
                .Items.ToListAsync();
            
            return new ServiceResponse<List<Item>>(items);
        }

        public async Task<ServiceResponse<Item>> CreateItem(Item item)
        {
            var existingItem = await _db.Items
                .FirstOrDefaultAsync(i => i.Id == item.Id);

            if (existingItem != null)
            {
                return new ServiceResponse<Item>("Item already exists");
            }

            _db.Items.Add(item);
            await _db.SaveChangesAsync();
            return new ServiceResponse<Item>(item);
        }

        public async Task<ServiceResponse<Item>> PutItem(Item item)
        {
            var existingItem = await _db.Items.FindAsync(item.Id);

            if (existingItem == null)
            {
                return new ServiceResponse<Item>("Item not found");
            }
            _db.Entry(existingItem).CurrentValues.SetValues(item);

            await _db.SaveChangesAsync();

            return new ServiceResponse<Item>(existingItem);
        }

        public async Task<ServiceResponse<bool>> DeleteItem(int id)
        {
        var deleteItem = await _db.Items.FirstOrDefaultAsync(i => i.Id == id);
        if (deleteItem != null)
        {
            _db.Items.Remove(deleteItem);
            await _db.SaveChangesAsync();
            return new ServiceResponse<bool>(true);
        }

        return new ServiceResponse<bool>("Item not found");
    }

    }
}
