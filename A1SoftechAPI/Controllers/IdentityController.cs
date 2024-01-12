using Core.Entities;
using Core.Interfaces;
using Core.ViewModel;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IUser user;
        private readonly JWTService jWTService;

        public IdentityController(IUser user, JWTService jWTService)
        {
            this.user=user;
            this.jWTService=jWTService;
        }
        [HttpPost("register")]
        public IActionResult Register([FromBody]RegisterViewModel registerViewModel)
        {
            user.CreateUser(registerViewModel);
            return Ok("Success");    
        }
        [HttpPost("Login")]
        public IActionResult Login([FromBody]LoginViewModel loginViewModel)
        {
            var User = user.GetUserByEmail(loginViewModel.Email);
            if (User == null) return BadRequest(new { message = "Invalid Email" });
            if (!BCrypt.Net.BCrypt.Verify(loginViewModel.password, User.Password))
            {
                return BadRequest(new { message = "Invalid UserName or Password" });
            }
            var jwt = jWTService.Generate(User.Id);
            Response.Cookies.Append("jwt", jwt, new CookieOptions {HttpOnly = true});
            return Ok(new {message = "Success"});
        }
        [HttpGet("GetUser")]
        public IActionResult GetUser()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];
                var token = jWTService.Verify(jwt);
                int userId = int.Parse(token.Issuer);
                var User = user.GetUserById(userId);
                return Ok(User);    
            }
            catch (Exception)
            {

                return Unauthorized();
            }
        }
        [HttpPost("LogOut")]
        public IActionResult LogOut()
        {
            Response.Cookies.Delete("jwt");
            return Ok(new { message = "Success" });
        }
        [HttpGet("GetRolesDropDown")]
        public IActionResult GetRolesDropDown()
        {
            try
            {
                return Ok(user.GetRoles());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data");
            }
        }

    }
}
