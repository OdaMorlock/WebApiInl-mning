using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_InlämningAttempt4.Data;
using WebApi_InlämningAttempt4.Models.Model;
using WebApi_InlämningAttempt4.Services;

namespace WebApi_InlämningAttempt4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IssuesController : ControllerBase
    {

        private readonly SqlDbContext _context;
        private readonly IIdentityServices _identity;

        public IssuesController(SqlDbContext context, IIdentityServices identity)
        {
            _context = context;
            _identity = identity;
        }

        [AllowAnonymous]
        [HttpGet("listissues")]
        public async Task<IActionResult> GetIssues()
        {
            return new OkObjectResult(await _identity.GetListOfIssues());
        }

        [AllowAnonymous]
        [HttpGet("searchlistissues")]
        public async Task<IActionResult> GetIssuesByQuery(string CustomerName, string Status, string Created, string Edited)
        {

            //https://docs.microsoft.com/en-us/dotnet/api/system.web.httprequest.querystring?view=netframework-4.8


           // string CustomerName = Request.Query[ "Customer"];
            //string Status = Request.Query["Status"];
            //string Created =  Request.Query["Created"];
            //string Edited = Request.Query["Edited"];

            if (CustomerName != null)
            {
                return new OkObjectResult("CustomerName Found" + " " + CustomerName );
            }
            if (Status != null)
            {
                return new OkObjectResult("Status Found" + " " + Status );
            }
            if (Created != null)
            {
                return new OkObjectResult("Created Found" + " " + Created );
            }
            if (Edited != null)
            {
                return new OkObjectResult("Edited Found" + " " + Edited );
            }

            return new BadRequestObjectResult("No Valid Search Paramaters found try   Customer  Or  Status  Or  Created  Or  Edited");

        }


        [AllowAnonymous]
        [HttpPost("issuecreate")]
        public async Task<IActionResult> IssueCreate([FromBody] CreateIssueModel createIssueModel)
        {
            if (await _identity.CreateIssueAsync(createIssueModel))
            {
                return new OkResult();
            }
            return new BadRequestResult();
        }



        [AllowAnonymous]
        [HttpPut("issueupdate")]
        public async Task<IActionResult> IssueUpdate([FromBody] UpdateIssueModel updateIssueModel)
        {
            if (await _identity.UpdateIssueAsync(updateIssueModel))
            {
                return new OkResult();
            }
            return new BadRequestResult();
        }
    }


}
