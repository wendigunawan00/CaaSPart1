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
    [ApiConventionType(typeof(WebApiConventions))]
    public class AuthorizationController : ControllerBase
    {
        private IAuth _auth;

        public AuthorizationController(IAuth auth, IMapper mapper)
        {
           this._auth = auth;
           _auth.setMapper(mapper ?? throw new ArgumentNullException(nameof(mapper)));
        }


        [HttpPost("login")]        
        public async Task<IActionResult> Login([FromBody] AdminDTO user)
        {
            var result = await _auth.Authenticate(user);           
            if (result is null) 
            { 
                return NotFound("Wrong Email or Password"); 
            }
            return Ok(result);
        }

        //[HttpGet()]
        //public async Task<IActionResult> GetLastMandant()
        //{
        //    var result = await _auth.Authenticate2();
        //    if (result==0)
        //    {
        //        return Unauthorized();
        //    }
        //    return Ok(result);
        //}
    }
}
