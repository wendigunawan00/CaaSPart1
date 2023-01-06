using AutoMapper;
using CaaS.Domain;
using CaaS.DTO;
using CaaS.Features;
using CaaS.Logic;
using Microsoft.AspNetCore.Mvc;

namespace CaaS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private IAuth _auth;

        public AuthorizationController(IAuth auth)
        {
           this._auth = auth;
        }

        
        
             
        [HttpPost("login")]        
        public async Task<IActionResult> Login([FromBody] AdminDTO user)
        {
            var result = await _auth.Authenticate(user);           
            if (result is null) 
            { 
                return Unauthorized(); 
            }
            return Ok(result);
        }
    }
}
