using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using API.DataTransferObj;
using API.Errors;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    /*
      .../api/users/ route -- for actions involving what the user can do. 
      Includes add/remove from the cart, transaction list or the item list.
      Also get and update user information as well.
    */

    [Authorize]
	public class UserController : ApiBaseController
	{
        private readonly IUserRepository _UserRepository;

		public UserController(IUserRepository userRepository)
		{
            this._UserRepository = userRepository;
		}

        //api/user
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersDetailDTO>>> GetUsers(){
            return Ok(await _UserRepository.GetUsersDTOAsync());
        }
        //api/user/self
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
        public async Task<ActionResult<ControllerBase>> addItemForSale([FromForm] string item, [FromForm] IFormFile image){
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var FILE_SIZE_MAX = 512000; //500KB cap

            ItemDTO itemDTO = System.Text.Json.JsonSerializer.Deserialize<ItemDTO>(item);
            DbResult result = new DbResult(true);
        
            var fullFolderPath = "";
            var fullFilePath = ""; //where to save on the system
            var publicPath = ""; //public url link to image

            //check if its a valid image, and create filesystem path for it 
            if((image?.Length > 0) && (image?.Length <= FILE_SIZE_MAX)){
                if(!(await isValidImage(image))) {
                    result.Success = false;
                    result.Details = "Invalid image file";
                }else{                    
                    var filename = string.Format(@"{0}-{1}", Guid.NewGuid(),image.FileName.Trim('"')); //give it unique name to avoid name collisions
                    var localRelativePath = Path.Combine("SavedResources","ItemImages",username); //folder relative to root directory
                    var publicRelativePath = Path.Combine("static_resources","ItemImages",username);
                    var h = HttpContext.Request;
                    var domainName = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host.Value;

                    fullFolderPath = Path.Combine(Directory.GetCurrentDirectory(),localRelativePath);
                    fullFilePath = Path.Combine(fullFolderPath,filename);
                    publicPath = Path.Combine(domainName,publicRelativePath,filename);
                }
            }else if(image?.Length > FILE_SIZE_MAX){
                result.Success = false;
                result.Details = "File must be a maximum of 10KB only";
            }
            itemDTO.ImagePath = publicPath;

            //add the item data to the database
            if(result.Success) result = await _UserRepository.AddItemForUserAsync(username,itemDTO);
            
            if(result.Success){
                if(image != null){ //save the image file
                    using(var ms = new MemoryStream()){
                        await image.CopyToAsync(ms);
                        var imageStream = Image.FromStream(ms);
                        
                        Directory.CreateDirectory(fullFolderPath); //create folder(s) if it doesn't exist
                        imageStream.Save(fullFilePath,ImageFormat.Jpeg);
                    }
                }
                return NoContent();
            }
            
            return BadRequest(result.Details);
        }

        //api/user/items
        [HttpGet("items")]
        public async Task<ActionResult<IEnumerable<ItemDTO>>> getUserItems(){
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

        
        private async Task<bool> isValidImage(IFormFile imageFile){
            using(var ms = new MemoryStream()){
                await imageFile.CopyToAsync(ms);
                
                try{
                    Image.FromStream(ms);
                    return true;
                }catch(ArgumentException){
                    return false;
                }
            }
        }
	}
}
