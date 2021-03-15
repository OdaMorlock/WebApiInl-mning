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

        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }


        // POST api/<UsersController>
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



        //// POST api/<UsersController>
        //[AllowAnonymous]
        //[HttpPost("issuecreate")]
        //public async Task<IActionResult> IssueCreate([FromBody] CreateIssueModel createIssueModel)
        //{
        //    if (await _identity.CreateIssueAsync(createIssueModel))
        //    {
        //        return new OkResult();
        //    }
        //    return new BadRequestResult();
        //}



        //[AllowAnonymous]
        //[HttpPut("issueupdate")]
        //public async Task<IActionResult> IssueUpdate([FromBody] UpdateIssueModel updateIssueModel)
        //{
        //    if( await _identity.UpdateIssueAsync(updateIssueModel))
        //    {
        //        return new OkResult();
        //    }
        //    return new BadRequestResult();
        //}

        // PUT api/<UsersController>/5

    }
}
