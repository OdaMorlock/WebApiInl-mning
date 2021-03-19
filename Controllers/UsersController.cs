using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_InlämningAttempt4.Data;
using WebApi_InlämningAttempt4.Models.Model;
using WebApi_InlämningAttempt4.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi_InlämningAttempt4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {

        private readonly SqlDbContext _context;
        private readonly IIdentityServices _identity;

        public UsersController(SqlDbContext context, IIdentityServices identity)
        {
            _context = context;
            _identity = identity;
        }

        private RequestUserModel IdentityRequest()
        {

            return new RequestUserModel
            {
                UserId = int.Parse(HttpContext.User.FindFirst("UserId").Value),
                AccessToken = Request.Headers["Authorization"].ToString().Split(" ")[1]

            };
        }


        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel signUpModel)
        {
            if (await _identity.CreateUserAsync(signUpModel))
            {
                return new OkResult();
            }
            return new BadRequestResult();
        }

        [AllowAnonymous]
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInModel signInModel)
        {
            var response = await _identity.SignInAsync(signInModel.Email, signInModel.Password);
            if (response.Succeded)
            {
                return new OkObjectResult(response.Result);
            }
            return new BadRequestObjectResult(response);
        }

        [HttpGet("getusers")]
        public async Task<IActionResult> GetUsersAsync()
        {
            if (_identity.ValidateAccessRights(IdentityRequest()))
            {
                return new OkObjectResult(await _identity.GetListOfUsersAsync());
            }
            return new UnauthorizedResult();
        }

        [HttpGet("getissueusers")]
        public async Task<IActionResult> GetIssueUserAsync()
        {
            if (_identity.ValidateAccessRights(IdentityRequest()))
            {
                return new OkObjectResult(await _identity.GetListOfIssueUserAsync());
            }
            return new UnauthorizedResult();
        }

    }
}
