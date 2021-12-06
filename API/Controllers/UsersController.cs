using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using API.DataTransferObj;
using API.Entities;
using API.Errors;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    /*
      .../api/users/ route -- for actions involving what the user can do. 
      Includes add/remove from the cart, transaction list or the item list.
      Also get and update user information as well.
    */

    [Authorize]
	public class UsersController : ApiBaseController
	{
        private readonly IUserRepository _UserRepository;
        private readonly IMapper _mapper;

		public UsersController(IUserRepository userRepository, IMapper mapper)
		{
            this._UserRepository = userRepository;
            this._mapper = mapper;
		}

        //api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersDetailDTO>>> GetUsers(){
            return Ok(await _UserRepository.GetUsersDTOAsync());
        }
        //api/users/1
        [HttpGet("self")]
        public async Task<ActionResult<UsersDetailDTO>> GetUser(){
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return await _UserRepository.GetUserDTOByUsernameAsync(username);
        }
        [HttpPut("self")]
        public async Task<ActionResult> UpdateUserDetails([FromBody] UserDetailUpdateDTO userDetailUpdateDTO){            
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            DbResult result = await _UserRepository.updateUser(username,userDetailUpdateDTO);

            if(result.Success) return NoContent();
            else return BadRequest(result.Details);
        }

        //------------- user cart -------------//
        [HttpPut("cart")]
        public async Task<ActionResult<ControllerBase>> addItemToCart([FromQuery] int id){
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            DbResult result = await _UserRepository.AddItemForUserCartAsync(username,id);
            if(result.Success) return NoContent();
            else return BadRequest(result.Details);
        }
        [HttpGet("cart")]
        public async Task<ActionResult<IEnumerable<CartDTO>>> getUserCart(){
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok(await _UserRepository.GetUserCartDTOAsync(username));
        }
        [HttpDelete("cart")]
        public async Task<ActionResult<ControllerBase>> RemoveItemFromUserCartAsync([FromQuery] int id){
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            DbResult result = await _UserRepository.RemoveItemFromUserCartAsync(username,id);
            if(result.Success)return NoContent();
            else return BadRequest(result.Details);
        }
        
        //------------- user items -------------//
        [HttpPost("items")]
        public async Task<ActionResult<ControllerBase>> addItemForSale([FromBody] ItemDTO itemDTO){
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            DbResult result = await _UserRepository.AddItemForUserAsync(username,itemDTO);
            if(result.Success) return NoContent();
            else return BadRequest(result.Details);
        }
        [HttpGet("items")]
        public async Task<ActionResult<IEnumerable<CartDTO>>> getUserItems(){
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok(await _UserRepository.GetUserItemsDTOAsync(username));
        }
        [HttpDelete("items")]
        public async Task<ActionResult<ControllerBase>> RemoveItemFromUser([FromQuery] int id){
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            DbResult result = await this._UserRepository.RemoveItemFromUser(username,id);
            if(result.Success) return NoContent();
            else return BadRequest(result.Details);
        }

        //------------- user transactions -------------// probably dont need the post (adding transactiosn should be done server side)
        [HttpPost("transactions")]
        public async Task<ActionResult<ControllerBase>> addNewTransaction([FromBody] TransactionDTO transactionDTO){
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            DbResult result = await _UserRepository.AddNewUserTransactionAsync(username,transactionDTO);
            if(result.Success) return NoContent();
            else return BadRequest(result.Details);
        }
        [HttpGet("transactions")]
        public async Task<ActionResult<IEnumerable<TransactionDTO>>> getUserTransactions(){
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok(await _UserRepository.GetUserTransactionsDTOAsync(username));
        }
        [HttpDelete("transactions")]
        public async Task<ActionResult<ControllerBase>> deleteTransaction([FromQuery] int id){
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            DbResult result = await this._UserRepository.RemoveUserTransactionAsync(username,id);
            if(result.Success) return NoContent();
            else return BadRequest(result.Details);
        }
	}
}
