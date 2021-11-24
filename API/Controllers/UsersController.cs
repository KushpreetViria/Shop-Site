using System.Collections.Generic;
using System.Threading.Tasks;
using API.DataTransferObj;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
	public class UsersController : ApiBaseController
	{
        private readonly IUserRepository _UserRepository;
        private readonly IItemRepository _itemRepository;
		private readonly IMapper _mapper;

		public UsersController(IUserRepository userRepository, IItemRepository itemRepository, IMapper mapper)
		{
            this._itemRepository = itemRepository;
            this._UserRepository = userRepository;
			this._mapper = mapper;
		}

        //api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersDetailDTO>>> GetUsers(){
            return Ok(await _UserRepository.GetUsersDTOAsync());
        }

        //api/users/1
        [HttpGet("{username}")]
        public async Task<ActionResult<UsersDetailDTO>> GetUser(string username){
            return await _UserRepository.GetUserDTOByUsernameAsync(username);
        }

        [HttpGet("{username}/orders")]
        public async Task<ActionResult<IEnumerable<TransactionDTO>>> getTransactions(string username){
            return Ok(await _UserRepository.GetUserTransactionsDTOAsync(username));
        }

        [HttpGet("{username}/cart")]
        public async Task<ActionResult<IEnumerable<CartDTO>>> getCart(string username){
            return Ok(await _UserRepository.GetUserCartDTOAsync(username));
        }

        //TODO: Abstract this away from here
        [HttpPost("{username}/items")]
        public async Task<ActionResult<ControllerBase>> addItemForSale(string username, [FromBody] ItemDTO itemDTO){
            AppUser user = await _UserRepository.GetUserAsync(username);

            if(user == null) return BadRequest();
            if(user.Items == null) user.Items = new List<Item>();

            Item itemEntity = _mapper.Map<ItemDTO,Item>(itemDTO);

            user.Items.Add(itemEntity);
            _UserRepository.update(user);

            return await _UserRepository.SaveAllAsync() ? Ok("Item Added") : BadRequest();
        }


        //Error: userRepository and ItemRepository have 2 different instances of DbContext,
        //calling update doesnt work because the contexts are not notified of changes in other
        //contexts
        [HttpPost("{username}/cart")]
        public async Task<ActionResult<ControllerBase>> addItemToCart(string username, [FromBody] ItemDTO itemDTO){
            Item item = await _itemRepository.GetItemAsync(itemDTO.Id);
            AppUser user = await _UserRepository.GetUserAsync(username);
            
            if(user.Cart == null) user.Cart = new Cart(){
                Count = 0,
                DateCreated = System.DateTime.Now
            };
            if(user.Cart.Items == null) user.Cart.Items = new List<Item>();

            if(user == null || item == null || 
                await _UserRepository.doesItemExistInCart(username,item.Id)) BadRequest();

            user.Cart.Items.Add(item);

            _itemRepository.update(item);
            _UserRepository.update(user);

            return await _UserRepository.SaveAllAsync() 
                && await _itemRepository.SaveAllAsync() ? Ok("Item Added") : BadRequest();
        }
	}
}
