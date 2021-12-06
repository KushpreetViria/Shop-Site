using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /*
        ...api/bug route -- only used for testing the server responces
    */

    public class BugController : ApiBaseController
    {
        public BugController(DataContext context){
            _context = context;
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret(){
            return (ActionResult<string>)"secret text";
        }

        [HttpGet("not-found")]
        public ActionResult<AppUser> GetNotFound(){
            var thing = _context.Users.Find(-1);
            if(thing == null) return NotFound();
            else return Ok(thing);
        }

        [HttpGet("server-error")]
        public ActionResult<string> GetServerError(){
            var thing = _context.Users.Find(-1);
            var returnVal = thing.ToString();
            return returnVal;
        }

        [HttpGet("bad-request")]
        public ActionResult<string> getBadRequest(){
            return BadRequest("Very bad request");
        }
        
    }
}