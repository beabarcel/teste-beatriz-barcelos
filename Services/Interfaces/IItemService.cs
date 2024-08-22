using Domain.Models;
using Services.Common;

namespace Services.Interfaces
{
    public interface IItemService
    {
        Task<ServiceResponse<List<Item>>> GetItems();
        Task<ServiceResponse<Item>> GetItemById(int id);
        Task<ServiceResponse<Item>> CreateItem(Item item);
        Task<ServiceResponse<Item>> PutItem(Item item);
        Task<ServiceResponse<bool>> DeleteItem(int id);
    }
}
