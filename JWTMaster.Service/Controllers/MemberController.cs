using JWTMaster.Service.Library;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace JWTMaster.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {


        private readonly IConfiguration _configuration;
        public MemberController(IConfiguration configuration)
        {


            _configuration = configuration;


        }


        [HttpGet("[action]")]
        public IActionResult SignIn()
        {
            string jeton = new JwtTokenMaker(_configuration).GenerateToken();
            return Ok(jeton);
        }

        [Authorize]
        [HttpGet("[action]")]
        public IActionResult JoinSuccess()
        {


            return Ok("Hello My Friend");
        }

    }
}
