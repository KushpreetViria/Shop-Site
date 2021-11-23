using System.Collections.Generic;
using System.Threading.Tasks;
using API.DataTransferObj;
using API.Entities;

namespace API.Interfaces
{
    public interface IItemRepository
    {
        Task<bool> SaveAllAsync();
        void update(Item item);

        Task<Item> GetItemAsync(int Id);
        Task<IEnumerable<Item>> GetItemsAsync();

        Task<IEnumerable<ItemDTO>> GetItemDTOAsync();
        Task<ItemDTO> GetItemDTOAsync(int Id);
    }
}