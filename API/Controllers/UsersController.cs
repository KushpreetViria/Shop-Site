using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data.Repository;
using API.DataTransferObj;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
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
        [HttpGet("{username}")]
        public async Task<ActionResult<UsersDetailDTO>> GetUser(string username){
            return await _UserRepository.GetUserDTOByUsernameAsync(username);
        }

        [HttpGet("{username}/orders")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> getOrders(string username){
            return Ok(await _UserRepository.GetUserOrderDTOAsync(username));
        }

        [HttpGet("{username}/cart")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> getCart(string username){
            return Ok(await _UserRepository.GetUserCartDTOAsync(username));
        }
	}
}