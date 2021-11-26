using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DataTransferObj;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class ItemsController : ApiBaseController
    {
        private readonly IItemRepository _itemRepository;
        public ItemsController(IItemRepository itemRepository){
            this._itemRepository = itemRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDTO>>> getItemsDTO(){
            return Ok(await _itemRepository.GetItemsDTOAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ItemDTO>>> getItemDTO(int id){
            return Ok(await _itemRepository.GetItemDTOAsync(id));
        }
    }
}