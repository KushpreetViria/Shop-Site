using System.Collections.Generic;
using System.Threading.Tasks;
using API.DataTransferObj;
using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> SaveAllAsync();
        void update(AppUser user);

        Task<AppUser> GetUserAsync(string username);
        Task<IEnumerable<AppUser>> GetUsersAsync();

        Task<IEnumerable<UsersDetailDTO>> GetUsersDTOAsync();
        Task<UsersDetailDTO> GetUserDTOByUsernameAsync(string username);   
        
        Task<IEnumerable<TransactionDTO>> GetUserTransactionsDTOAsync(string username);
        Task<CartDTO> GetUserCartDTOAsync(string username);

        Task<bool> doesItemExistInCart(string username, int Id);
    }
}