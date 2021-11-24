using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DataTransferObj;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repository
{
	public class UserRepository : IUserRepository
	{
        private readonly DataContext _context;
		private readonly IMapper _mapper;

		public UserRepository(DataContext context, IMapper mapper){
            this._context = context;
			this._mapper = mapper;
		}

		public async Task<bool> SaveAllAsync()
		{
			return await _context.SaveChangesAsync() > 0;
		}

		public void update(AppUser user)
		{
			_context.Entry(user).State = EntityState.Modified;
		}

		public async Task<IEnumerable<AppUser>> GetUsersAsync()
		{
			return await _context.Users
				.Include(x=>x.Cart)
				.Include(x=>x.Transactions)
				.ToListAsync();
		}

		public async Task<AppUser> GetUserAsync(string username)
		{
			return await _context.Users
				.Where(x => x.UserName == username)
				.FirstOrDefaultAsync();
		}

		public async Task<IEnumerable<UsersDetailDTO>> GetUsersDTOAsync()
		{
			return await _context.Users
				.ProjectTo<UsersDetailDTO>(_mapper.ConfigurationProvider)
				.ToListAsync();
		}

		public async Task<UsersDetailDTO> GetUserDTOByUsernameAsync(string username)
		{
			return await _context.Users
				.Where(x => x.UserName == username)
				.ProjectTo<UsersDetailDTO>(_mapper.ConfigurationProvider)
				.SingleOrDefaultAsync();
		}

		public async Task<IEnumerable<TransactionDTO>> GetUserTransactionsDTOAsync(string username)
		{
			return await _context.Users
				.Where(p => p.UserName == username)
				.SelectMany(p => p.Transactions)
				.ProjectTo<TransactionDTO>(_mapper.ConfigurationProvider)
				.ToListAsync();
		}

		public async Task<CartDTO> GetUserCartDTOAsync(string username)
		{
			return await _context.Users
				.Where(p => p.UserName == username)
				.Select(p => p.Cart)
				.ProjectTo<CartDTO>(_mapper.ConfigurationProvider)
				.SingleOrDefaultAsync();
		}

		// this doesnt look right
		public async Task<bool> doesItemExistInCart(string username, int Id){
			return await _context.Users
				.Where(x => x.UserName == username)
				.Select(x => x.Cart.Items)
				.AnyAsync(item => item.Any(x => x.Id == Id));
		}
	}
}