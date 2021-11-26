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
		public void update(Entity user)
		{
			_context.Entry(user).State = EntityState.Modified;
		}



		//-------------- Usesr DTO Actions --------------//
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



		//-------------- Usesr Cart Actions --------------//
		public async Task<CartDTO> GetUserCartDTOAsync(string username)
		{
			AppUser user = await this.GetUserAsync(username);
			if(user != null && user.Cart != null)
				return await _context.Users
					.Where(p => p.UserName == username)
					.Select(p => p.Cart)
					.ProjectTo<CartDTO>(_mapper.ConfigurationProvider)
					.FirstOrDefaultAsync();
			else{
				return null;
			}
		}
		public async Task<bool> AddItemForUserCartAsync(string username, int itemID)
		{
			Item item = await _context.Items.FindAsync(itemID);
            AppUser user = await this.GetUserAsync(username);

			// ignore if duplicate addition or null references
			if(user == null || item == null ||
                await this.doesItemExistInCart(username,itemID)) return false;

			// instantiate user cart
			if(user.Cart == null) 
				user.Cart = new Cart()
				{
					Count = 0,
					DateCreated = System.DateTime.Now,
				};
			if(user.Cart.Items == null) user.Cart.Items = new List<Item>();

			//add the item to the cart
			user.Cart.Count++;
            user.Cart.Items.Add(item);

            return await this.SaveAllAsync() ? true : false;
		}
		public async Task<bool> RemoveItemFromUserCartAsync(string username, int itemID)
		{
			AppUser user = await this.GetUserAsync(username);
			Item item = await _context.Items.FindAsync(itemID);
			
			if(user != null && item!=null && user.Cart.Items != null &&
				await doesItemExistInCart(username,itemID)){
					user.Cart.Items?.Remove(item);
					if(user.Cart.Count > 0) user.Cart.Count--;
					if(user.Cart.Count <= 0){
						user.Cart = null;
					}
				}

			return await this.SaveAllAsync();
		}



		//--------------// User Item actions -------------- //
		public async Task<IEnumerable<ItemDTO>> GetUserItemsDTOAsync(string username)
		{
			AppUser user = await this.GetUserAsync(username);
			if(user != null && user.Items != null){
				return await _context.Users
					.Where(p => p.UserName == username)
					.SelectMany(p => p.Items)
					.ProjectTo<ItemDTO>(_mapper.ConfigurationProvider)
					.ToListAsync();
			}else{
				return null;
			}
		}
		public async Task<bool> AddItemForUserAsync(string username, ItemDTO itemDTO)
		{
			AppUser user = await this.GetUserAsync(username);

            if(user == null) return false;
            if(user.Items == null) user.Items = new List<Item>();

            Item itemEntity = _mapper.Map<ItemDTO,Item>(itemDTO);
			itemEntity.DateListed = System.DateTime.Today;

            user.Items.Add(itemEntity);
            
            this.update(user);

            return await this.SaveAllAsync() ? true : false;
		}
		public async Task<bool> RemoveItemFromUser(string username, int itemID)
		{
			AppUser user = await this.GetUserAsync(username);
			Item item = await _context.Items
				.Where(x => x.Id == itemID)
				.Include(x => x.Carts)
				.SingleOrDefaultAsync();

			if(user != null && user.Items != null && item != null){
				foreach(var cart in item.Carts){
					cart.Count--;
				}

				user.Items.Remove(item); //is this needed? test later
				_context.Items.Remove(item);
			}

			return await this.SaveAllAsync();
		}



		//-------------- User Transaction actions -------------- //
		public async Task<IEnumerable<TransactionDTO>> GetUserTransactionsDTOAsync(string username)
		{
			return await _context.Users
				.Where(p => p.UserName == username)
				.SelectMany(p => p.Transactions)
				.ProjectTo<TransactionDTO>(_mapper.ConfigurationProvider)
				.ToListAsync();
		}
		public async Task<bool> AddNewUserTransactionAsync(string username, TransactionDTO transactionDTO)
		{
			AppUser user = await this.GetUserAsync(username);

			Transaction transaction = _mapper.Map<TransactionDTO,Transaction>(transactionDTO);			
			if(transaction.TransactionDetails == null) transaction.TransactionDetails = new List<TransactionDetails>();

			transaction.TotalCost = 0;
			foreach(var transactioninfo in transaction.TransactionDetails){
				transaction.TotalCost += (transactioninfo.UnitPrice * transactioninfo.Quantity);	
			}
			transaction.TransactionDate = System.DateTime.Now;

			if(user.Transactions == null) user.Transactions = new List<Transaction>();
			user.Transactions.Add(transaction);

			return await this.SaveAllAsync() ? true : false;
		}
		public async Task<bool> RemoveUserTransactionAsync(string username, int TransactionID)
		{
			AppUser user = await this.GetUserAsync(username);

			if(user != null && user.Transactions != null){
				Transaction transactionToRemove = await _context.Users
					.Where(x=> x.UserName == username)
					.SelectMany(x => x.Transactions)
					.Where(x => x.Id == TransactionID)
					.SingleOrDefaultAsync();
					
				user.Transactions.Remove(transactionToRemove);
			}
			return await this.SaveAllAsync();
		}



		//-------------- helper methods --------------//
		private async Task<bool> doesItemExistInCart(string username, int Id){
			return await _context.Users
				.Where(x => x.UserName == username)
				.SelectMany(x => x.Cart.Items)
				.AnyAsync(item => item.Id == Id);
		}
		
		//Generic and inefficient in queries where not all the included data is needed
		//TODO: use more personalized methods that only includes whats actually needed
		private async Task<AppUser> GetUserAsync(string username)
		{
			return await _context.Users				
				.Include(x => x.Items)
				.ThenInclude(x=> x.ItemImage)
				.Include(x => x.Cart)
				.ThenInclude(x => x.Items)
				.Include(x=>x.Transactions)
				.ThenInclude(x=>x.TransactionDetails)
				.Where(x => x.UserName == username)
				.AsSplitQuery()
				.OrderBy(x=> x.Id) //to remove the annoying warning
				.SingleOrDefaultAsync();
		}
	}
}