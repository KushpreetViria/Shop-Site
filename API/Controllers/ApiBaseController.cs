using API.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
	[Route("api/[controller]")]
    public class ApiBaseController : ControllerBase
    {
        protected DataContext _context { get; set; }
    }
}