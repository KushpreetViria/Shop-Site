using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DataTransferObj;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class ItemsController : ApiBaseController
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ItemsController(DataContext context, IMapper mapper){
            this._context = context;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDTO>>> getItems(){
            return await _context.Items.Include(x => x.ItemImage)
				.ProjectTo<ItemDTO>(_mapper.ConfigurationProvider)
				.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ItemDTO>>> getItem(int id){
            return await _context.Items
                .Where(p => p.Id == id)
				.ProjectTo<ItemDTO>(_mapper.ConfigurationProvider)
				.ToListAsync();
        }
    }
}