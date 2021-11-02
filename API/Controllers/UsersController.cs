using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UsersController : ControllerBase
	{
        private readonly DataContext _context;
		public UsersController(DataContext context)
		{
            this._context = context;
		}

        //api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers(){
            //ToListAsync is a async task which runs in a new thread
            //await keyword means this method blocks until the toListAsync is finished
            var users = await _context.Users.ToListAsync();
            return users;
        }

        //api/users/1
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id){
            var user = await _context.Users.FindAsync(id);
            return user;
        }
	}
}