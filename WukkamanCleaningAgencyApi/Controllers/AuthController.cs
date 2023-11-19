using WukkamanCleaningAgencyApi.Models;
using WukkamanCleaningAgencyApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WukkamanCleaningAgencyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            this._authService = authService;
        }

        [HttpPost ("register")]
        [Authorize (Roles = "Admin")]
        public async Task<IActionResult> Register(User user)
        {
            if (await _authService.RegisterUser(user))
            {
                return Ok(new { status = "Success", message = "User Registration Successfull" });
            }

            return BadRequest(new { status = "Failed", message = "User Registration Failed" });
        }

        [HttpPost ("login")]
        public async Task<IActionResult> Login(User user)
        {
            var result = await _authService.Login(user);
            if(result == true)
            {
                var token = _authService.GenerateToken(user);
                return Ok(new {status = "Success", message = "Login Successful", data = token});
            }
            return BadRequest(new {status = "fail", message = "Login Failed"});

        }
        
    }
}
