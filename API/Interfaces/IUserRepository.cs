using System.Collections.Generic;
using System.Threading.Tasks;
using API.DataTransferObj;
using API.Entities;
using API.Errors;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        // Task<bool> SaveAllAsync();
        // void update(Entity entity);

        Task<AppUser> GetUserAsync(string username);
        Task<IEnumerable<UsersDetailDTO>> GetUsersDTOAsync();
        Task<UsersDetailDTO> GetUserDTOByUsernameAsync(string username);
        Task<DbResult> updateUser(string username, UserDetailUpdateDTO userDetailUpdateDTO);

        Task<CartDTO> GetUserCartDTOAsync(string username);
        Task<DbResult> AddItemForUserCartAsync(string username, int itemID);
        Task<DbResult> RemoveItemFromUserCartAsync(string username, int itemID);
        
        Task<IEnumerable<ItemDTO>> GetUserItemsDTOAsync(string username);
        Task<DbResult> AddItemForUserAsync(string username, ItemDTO itemDTO);
        Task<DbResult> RemoveItemFromUser(string username, int itemID);

        Task<IEnumerable<TransactionDTO>> GetUserTransactionsDTOAsync(string username);
        Task<DbResult> AddNewUserTransactionAsync(string username, TransactionDTO transactionDTO);
        Task<DbResult> RemoveUserTransactionAsync(string username, int TransactionID);
    }
}