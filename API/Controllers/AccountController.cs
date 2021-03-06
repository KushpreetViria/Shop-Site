//api/account route --

using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DataTransferObj;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{    

    /*
        .../api/account route -- used for account actions like login and register
    */

	public class AccountController : ApiBaseController
	{
		private readonly ITokenService _tokenService;

		public AccountController(DataContext context, ITokenService tokenService)
		{
			this._context = context;
			this._tokenService = tokenService;
		}

        [HttpPost("register")]
        public async Task<ActionResult<UserSessionDTO>> Register([FromBody] RegisterDTO registerDTO){
            if(await existingUser(registerDTO.username)) return BadRequest("Username is taken");
            using var hmac = new HMACSHA512();
            var user = new AppUser{
                UserName = registerDTO.username,
                passHash = hmac.ComputeHash(
                    Encoding.UTF8.GetBytes(registerDTO.password)),
                    passSalt = hmac.Key
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserSessionDTO
            {
                username = user.UserName,
                token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserSessionDTO>> Login([FromBody] LoginDTO loginDTO){
            var user = await _context.Users.SingleOrDefaultAsync(x =>
                x.UserName == loginDTO.username);
            
            if (user == null) return Unauthorized("Invalid username/password");
                        
            using var hmac = new HMACSHA512(user.passSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.password));

            for(uint i = 0; i < computedHash.Length; i++ ){
                if(computedHash[i] != user.passHash[i])
                    return Unauthorized("Invalid username/password");
            }

            return new UserSessionDTO
            {
                username = user.UserName,
                token = _tokenService.CreateToken(user)
            };
        }

        private async Task<bool> existingUser(string username){
            return await _context.Users.AnyAsync(x => x.UserName == username);
        }
	}
}