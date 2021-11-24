using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DataTransferObj;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repository
{
	public class ItemRepository : IItemRepository
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;

		public ItemRepository(DataContext context, IMapper mapper){
            this._context = context;
			this._mapper = mapper;
		}

		public async Task<Item> GetItemAsync(int Id)
		{
			return await _context.Items
				.Where(x => x.Id == Id)
				.FirstOrDefaultAsync();
		}

		public Task<IEnumerable<ItemDTO>> GetItemDTOAsync()
		{
			throw new NotImplementedException();
		}

		public Task<ItemDTO> GetItemDTOAsync(int Id)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Item>> GetItemsAsync()
		{
			throw new NotImplementedException();
		}

		public async Task<bool> SaveAllAsync()
		{
			return await _context.SaveChangesAsync() > 0;
		}

		public void update(Item item)
		{
			_context.Entry(item).State = EntityState.Modified;
		}
	}
}