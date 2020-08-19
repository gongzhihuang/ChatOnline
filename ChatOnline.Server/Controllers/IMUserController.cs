using ChatOnline.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChatOnline.Server.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class IMUserController : ControllerBase
    {
        private readonly ILogger<IMUserController> _logger;

        private readonly IIMUserService _iMUserService;

        public IMUserController(ILogger<IMUserController> logger, IIMUserService iMUserService)
        {
            _logger = logger;
            _iMUserService = iMUserService;
        }
        
        
        [HttpGet("friends")]
        public IActionResult GetFriends(long iMNumber)
        {
            var res = _iMUserService.GetFriends(iMNumber);
            return Ok(res);
        }
    }
}