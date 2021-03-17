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
        public async Task<IActionResult> GetIssuesAsync()
        {
            return new OkObjectResult(await _identity.GetListOfIssuesAsync());
        }

        [AllowAnonymous]
        [HttpGet("searchlistissues")]
        public async Task<IActionResult> GetIssuesByQueryAsync(string Customer, string Status, string Created, string Edited)
        {

            //https://stackoverflow.com/questions/38738725/having-multiple-get-methods-with-multiple-query-string-parameters-in-asp-net-cor
            //https://docs.microsoft.com/en-us/dotnet/standard/base-types/parsing-datetime

            if (Customer != null)
            {

                try
                {
                    var list = await _identity.GetListOfIssuesByCustomerName(Customer);

                    return new OkObjectResult(list);
                }
                catch 
                {

                   
                }
                return new BadRequestObjectResult($"{Customer} Did not Match Any Customer  ");


            }
            if (Status != null)
            {

                try
                {
                    if (Status.Equals("Active") | Status.Equals("InActive") | Status.Equals("Finished"))
                    {
                        return new OkObjectResult(await _identity.GetListOfIssuesByStatus(Status));
                    }
                   
                }
                catch 
                {

                    
                }
                return new BadRequestObjectResult($"{Status} Did not Match Any Status  ");

            }
            if (Created != null)
            {
                try
                {
                    
                    DateTime _created = DateTime.Parse(Created);
                    var _date = _created.Date;
                    var _hour = _created.Hour;

                    //return new OkObjectResult( await _identity.GetListOfIssuesByDateCreated(_created));
                    return new OkObjectResult("Created Found" + " " + " Date " + _date + " " + " Hour " + _hour);

                }
                catch 
                {

                   
                }
                return new BadRequestObjectResult($"Chould Not Convert {Created} into Date Time try   0000-00-00  format  ");

            }
            if (Edited != null)
            {

                try
                {
                    DateTime _edited = DateTime.Parse(Edited);
                    return new OkObjectResult(await _identity.GetListOfIssuesByDateEdited(_edited));
                }
                catch 
                {

                    
                }
                return new BadRequestObjectResult("Chould not convert Eited into DateTime try   0000-00-00    format  ");

            }

            return new BadRequestObjectResult("No Valid Search Paramaters found try   Customer  Or  Status  Or  Created  Or  Edited");

        }


        [AllowAnonymous]
        [HttpPost("issuecreate")]
        public async Task<IActionResult> IssueCreateAsync([FromBody] CreateIssueModel createIssueModel)
        {
            if (await _identity.CreateIssueAsync(createIssueModel))
            {
                return new OkResult();
            }
            return new BadRequestResult();
        }



        [AllowAnonymous]
        [HttpPut("issueupdate")]
        public async Task<IActionResult> IssueUpdateAsync([FromBody] UpdateIssueModel updateIssueModel)
        {
            if (await _identity.UpdateIssueAsync(updateIssueModel))
            {
                return new OkResult();
            }
            return new BadRequestResult();
        }
    }


}
