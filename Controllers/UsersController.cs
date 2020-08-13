using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using expense_tracker.Infrastructure;
using expense_tracker.Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace expense_tracker.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;
        private readonly IUsersService _usersService;

        public UsersController(IJwtAuthenticationManager jwtAuthenticationManager, IUsersService usersService)
        {
            _jwtAuthenticationManager = jwtAuthenticationManager;
            _usersService = usersService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCredential userCredentials)
        {
            try
            {
                await _usersService.CreateUser(userCredentials);
            }
            catch(HttpStatusException e)
            {
                if(e.Status == HttpStatusCode.BadRequest)
                {
                    return BadRequest(e.Message);
                }
            }
            
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UserCredential userCredentials)
        {
            var token = await _jwtAuthenticationManager.Authenticate(userCredentials.Username, userCredentials.Password);
            
            if(token == null)
            {
                return Unauthorized();
            }
            
            return Ok(token);
        }
    }
}
