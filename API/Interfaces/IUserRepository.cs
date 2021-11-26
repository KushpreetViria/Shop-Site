using System.Collections.Generic;
using System.Threading.Tasks;
using API.DataTransferObj;
using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> SaveAllAsync();
        void update(Entity entity);

        Task<IEnumerable<UsersDetailDTO>> GetUsersDTOAsync();
        Task<UsersDetailDTO> GetUserDTOByUsernameAsync(string username);
                
        Task<CartDTO> GetUserCartDTOAsync(string username);
        Task<bool> AddItemForUserCartAsync(string username, int itemID);
        Task<bool> RemoveItemFromUserCartAsync(string username, int itemID);
        
        Task<IEnumerable<ItemDTO>> GetUserItemsDTOAsync(string username);
        Task<bool> AddItemForUserAsync(string username, ItemDTO itemDTO);
        Task<bool> RemoveItemFromUser(string username, int itemID);

        Task<IEnumerable<TransactionDTO>> GetUserTransactionsDTOAsync(string username);
        Task<bool> AddNewUserTransactionAsync(string username, TransactionDTO transactionDTO);
        Task<bool> RemoveUserTransactionAsync(string username, int TransactionID);
    }
}