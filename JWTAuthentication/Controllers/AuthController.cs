using JWTAuthentication.DTO;
using JWTAuthentication.Model;
using JWTAuthentication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserAuthService _userAuthService;
        public AuthController(IUserAuthService userAuthService)
        {
            _userAuthService = userAuthService;
        }

        //[HttpGet(Name = "AuthCheck")]
        //public string CheckAuthController() 
        //{
        //    return "Auth controller working";
        //}

        [HttpGet, Authorize]
        public IActionResult GetUserName() 
        {
            var userDetail = _userAuthService.GetUserDetails();
            
            return Ok(userDetail);
        }

        [HttpPost("register", Name = "RegisterUser")]
        public async Task<IActionResult> RegisterUser(UserDTO userRequestDTO) 
        {
            if (userRequestDTO is not null)
            {
                //var user = await _userAuthService.GetPasswordHash(userRequestDTO);
                var token = _userAuthService.GetJWTToken(userRequestDTO);
                return Ok(token);
            }
            else 
            {
                return BadRequest();
            }

        }

    }
}
