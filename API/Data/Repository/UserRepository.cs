using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DataTransferObj;
using API.Entities;
using API.Errors;
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

		private async Task<bool> SaveAllAsync()
		{
			return await _context.SaveChangesAsync() > 0;
		}
		private void update(Entity entity)
		{
			_context.Entry(entity).State = EntityState.Modified;
		}


		public async Task<AppUser> GetUserAsync(string username)
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

		public async Task<DbResult> updateUser(string username, UserDetailUpdateDTO userDetailUpdateDTO){
			AppUser user = await this.GetUserAsync(username);

			DbResult nullEntites = isEntityNull(user);
			if(nullEntites.Success){
				return new DbResult(false,nullEntites.Details);
			}

            this._mapper.Map(userDetailUpdateDTO,user);
			user.FullAddress = user.GetFullAddress();
			
            this.update(user);			

            return await this.SaveAllAsync() ? new DbResult(true) : new DbResult(false,"Failed to update user");
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


		//-------------- User Cart Actions --------------//
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
		public async Task<DbResult> AddItemForUserCartAsync(string username, int itemID)
		{
			Item item = await _context.Items.FindAsync(itemID);
            AppUser user = await this.GetUserAsync(username);

			// ignore if duplicate addition or null references			
			DbResult nullEntites = isEntityNull(user,item);
			if(nullEntites.Success){
				return new DbResult(false,nullEntites.Details);
			}else if((await this.doesItemExistInCart(username,itemID)).Success){
				return new DbResult(false, "Item already added to cart");
			}

			// instantiate user cart
			if(user.Cart == null) 
				user.Cart = new Cart()
				{
					TotalCost = 0,
					Count = 0,
					DateCreated = System.DateTime.Now,
				};
			if(user.Cart.Items == null) user.Cart.Items = new List<Item>();

			//add the item to the cart
			user.Cart.Count++;
            user.Cart.Items.Add(item);
			user.Cart.TotalCost = addCurrency(user.Cart.TotalCost,item.Price);

            return await this.SaveAllAsync() ? new DbResult(true) : new DbResult(false,"Failed to add item.");
		}
		public async Task<DbResult> RemoveItemFromUserCartAsync(string username, int itemID)
		{
			AppUser user = await this.GetUserAsync(username);
			Item item = await _context.Items.FindAsync(itemID);
			
			DbResult nullEntities = isEntityNull(user,item,user?.Cart);
			if(nullEntities.Success){
				return new DbResult(false,nullEntities.Details);
			}else if(user.Cart.Items == null){
				return new DbResult(false,"Cart is empty already.");
			}else if(!(await doesItemExistInCart(username,itemID)).Success){
				return new DbResult(false, "This item is not in the cart.");
			}

			//item does exist, remove it
			user.Cart.Items.Remove(item);
			if(user.Cart.Count > 0){
				user.Cart.Count--;
				user.Cart.TotalCost = substractCurrency(user.Cart.TotalCost,item.Price);
			}
			if(user.Cart.Count <= 0){
				user.Cart = null;
			}

			update(user);
			update(item);
			return await this.SaveAllAsync() ? new DbResult(true) : new DbResult(false, "Failed to remove item.");
		}



		//--------------// User Item actions -------------- //
		public async Task<IEnumerable<ItemDTO>> GetUserItemsDTOAsync(string username)
		{
			AppUser user = await this.GetUserAsync(username);
			if(user?.Items != null){
				return await _context.Users
					.Where(p => p.UserName == username)
					.SelectMany(p => p.Items)
					.ProjectTo<ItemDTO>(_mapper.ConfigurationProvider)
					.ToListAsync();
			}else{
				return null;
			}
		}
		public async Task<DbResult> AddItemForUserAsync(string username, ItemDTO itemDTO)
		{
			AppUser user = await this.GetUserAsync(username);

			DbResult nullEntities = isEntityNull(user);
            if(nullEntities.Success){
				return new DbResult(false, nullEntities.Details);
			}

            if(user.Items == null) user.Items = new List<Item>();

            Item itemEntity = _mapper.Map<ItemDTO,Item>(itemDTO);
			itemEntity.DateListed = System.DateTime.Today;

            user.Items.Add(itemEntity);
            
            this.update(user);

            return await this.SaveAllAsync() ? new DbResult(true) : new DbResult(false,"Failed to add item for user.");
		}
		public async Task<DbResult> RemoveItemFromUser(string username, int itemID)
		{
			AppUser user = await this.GetUserAsync(username);
			Item item = await _context.Items
				.Where(x => x.Id == itemID)
				.Include(x => x.Carts)
				.SingleOrDefaultAsync();

			DbResult nullEntities = isEntityNull(user,item);
            if(nullEntities.Success){
				return new DbResult(false, nullEntities.Details);
			}

			if(user.Items != null){
				foreach(Cart cart in item.Carts){
					cart.Count--;
					cart.TotalCost = substractCurrency(cart.TotalCost,item.Price);
				}

				user.Items.Remove(item); //is this needed? test later
				_context.Items.Remove(item);
			}

			return await this.SaveAllAsync() ? new DbResult(true) : new DbResult(false,"Failed to remove item from system.");
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
		public async Task<DbResult> AddNewUserTransactionAsync(string username, TransactionDTO transactionDTO)
		{
			AppUser user = await this.GetUserAsync(username);
			Transaction transaction = _mapper.Map<TransactionDTO,Transaction>(transactionDTO);			

			DbResult nullEntities = isEntityNull(user,transaction);
            if(nullEntities.Success){
				return new DbResult(false, nullEntities.Details);
			}

			if(transaction.TransactionDetails == null) transaction.TransactionDetails = new List<TransactionDetails>();

			transaction.TotalCost = 0;
			foreach(var transactioninfo in transaction.TransactionDetails){
				transaction.TotalCost += (transactioninfo.UnitPrice * transactioninfo.Quantity);	
			}
			transaction.TransactionDate = System.DateTime.Now;

			if(user.Transactions == null) user.Transactions = new List<Transaction>();
			user.Transactions.Add(transaction);

			return await this.SaveAllAsync() ? new DbResult(true) : new DbResult(false, "Failed to add transaction history.");
		}

		public async Task<DbResult> RemoveUserTransactionAsync(string username, int TransactionID)
		{
			AppUser user = await this.GetUserAsync(username);
			DbResult nullEntities = isEntityNull(user);
            if(nullEntities.Success){
				return new DbResult(false, nullEntities.Details);
			}

			if(user.Transactions != null){
				Transaction transactionToRemove = await _context.Users?
					.Where(x=> x.UserName == username)
					.SelectMany(x => x.Transactions)
					.Where(x => x.Id == TransactionID)
					.SingleOrDefaultAsync();
				
				if(isEntityNull(transactionToRemove).Success) return new DbResult(false, "No such transaction exists");
					
				user.Transactions.Remove(transactionToRemove);
			}else{
				return new DbResult(false,"No transaction to remove");
			}
			return await this.SaveAllAsync() ? new DbResult(true) : new DbResult(false,"Failed to remove transaction");
		}



		//-------------- helper functions --------------//
		private async Task<DbResult> doesItemExistInCart(string username, int Id){
			bool result =  await _context.Users
				.Where(x => x.UserName == username)
				.SelectMany(x => x.Cart.Items)
				.AnyAsync(item => item.Id == Id);
			return new DbResult(result);
		}

		private static DbResult isEntityNull(params Entity[] entities){
			foreach(Entity entity in entities){
				if(entity == null){
					return new DbResult(true,$"The {entity.GetType().Name} does not exist.");
				}
			}
			return new DbResult(false);
		}

		private static decimal addCurrency(params decimal[] numbers){
			decimal total = 0;
			foreach(decimal num in numbers){
				total += num*100;
			}
			return total/100;
		}

		private static decimal substractCurrency(decimal original, params decimal[] numbers){
			decimal total = original;
			foreach(decimal num in numbers){
				total -= num*100;
			}
			return total/100;
		}
	}
}