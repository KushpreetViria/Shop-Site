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

        Task<ItemDTO> GetItemDTOAsync(int Id);
        Task<IEnumerable<ItemDTO>> GetItemsDTOAsync();
    }
}