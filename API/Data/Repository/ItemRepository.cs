using System;
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

	/*
        A repository that handles communication to the database item table and mapping to DTOs
    */

	public class ItemRepository : IItemRepository
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;

		public ItemRepository(DataContext context, IMapper mapper){
            this._context = context;
			this._mapper = mapper;
		}

		public async Task<IEnumerable<ItemDTO>> GetItemsDTOAsync()
		{
			return await _context.Items.Include(x => x.ItemImage)
				.ProjectTo<ItemDTO>(_mapper.ConfigurationProvider)
				.ToListAsync();
		}

		public async Task<ItemDTO> GetItemDTOAsync(int Id)
		{
			return await _context.Items
                .Where(p => p.Id == Id)
				.ProjectTo<ItemDTO>(_mapper.ConfigurationProvider)
				.FirstOrDefaultAsync();
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