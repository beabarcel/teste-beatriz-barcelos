using Domain.Models;

namespace Services.Interfaces
{
    public interface IItemService
    {
        Task<List<Item>> GetItems();
        Task<Item> GetItemById(int id);
        Task<Item> CreateItem(Item item);
        Task<Item> PutItem(Item item);
        Task<bool> DeleteItem(int id);
    }
}
