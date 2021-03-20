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

        private RequestUserModel IdentityRequest()
        {

            return new RequestUserModel
            {
                UserId = int.Parse(HttpContext.User.FindFirst("UserId").Value),
                AccessToken = Request.Headers["Authorization"].ToString().Split(" ")[1]

            };
        }


        
        [HttpGet("listissues")]
        public async Task<IActionResult> GetIssuesAsync()
        {
            if (_identity.ValidateAccessRights(IdentityRequest()))
            {
                return new OkObjectResult(await _identity.GetListOfIssuesAsync());
            }
            return new UnauthorizedResult();
        }

        
        [HttpGet("searchlistissues")]
        public async Task<IActionResult> GetIssuesByQueryAsync(string Customer, string Status, string Created, string Edited)
        {
            var test = true;
            //https://stackoverflow.com/questions/38738725/having-multiple-get-methods-with-multiple-query-string-parameters-in-asp-net-cor
            //https://docs.microsoft.com/en-us/dotnet/standard/base-types/parsing-datetime

            if (_identity.ValidateAccessRights(IdentityRequest()))
            {
                if (Customer != null)
                {

                    try
                    {
                    
                        var list = await _identity.GetListOfIssuesByCustomerNameAsync(Customer);

                        if (!list.Any())
                        {
                            return new BadRequestObjectResult($"{Customer} Did not Match Any Customer  ");
                        }

                        return new OkObjectResult(list);
                    }
                    catch
                    {


                    }
                    return new BadRequestObjectResult($"Did not enter Try Catch  {Customer} ");


                }
                if (Status != null)
                {

                    try
                    {
                        if (Status.Equals("Active") | Status.Equals("InActive") | Status.Equals("Finished"))
                        {
                            return new OkObjectResult(await _identity.GetListOfIssuesByStatusAsync(Status));
                        }

                    }
                    catch
                    {


                    }
                    return new BadRequestObjectResult($"{Status} Did not Match Any Status Try    Active  Or  InActive  Or  Finished");

                }
                if (Created != null)
                {
                    try
                    {

                        DateTime _created = DateTime.Parse(Created);
                       var _list = await _identity.GetListOfIssuesByDateCreatedAsync(_created);

                        if (!_list.Any())
                        {
                            return new BadRequestObjectResult($"{Created} Did not Match any Issue");
                        }
                        return new OkObjectResult(_list);



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
                        var _list = await _identity.GetListOfIssuesByDateCreatedAsync(_edited);
                        if (!_list.Any())
                        {
                            return new BadRequestObjectResult($"{Edited} Did not match any Issue");
                        }
                        return new OkObjectResult(_list);


                    }
                    catch
                    {


                    }
                    return new BadRequestObjectResult($"Chould Not Convert {Edited} into Date Time try   0000-00-00  format  ");

                }


                return new BadRequestObjectResult("No Valid Search Paramaters found try   Customer  Or  Status  Or  Created  Or  Edited");
            }
            return new UnauthorizedResult();

          

        }

        
        [HttpGet("orderlistby")]
        public async Task<IActionResult> GetIssueByOrderAsync(string OrderBy,string Status,string Customer)
        {
            var test = true;
            //https://stackoverflow.com/questions/38738725/having-multiple-get-methods-with-multiple-query-string-parameters-in-asp-net-cor
            //https://docs.microsoft.com/en-us/dotnet/standard/base-types/parsing-datetime

            if (_identity.ValidateAccessRights(IdentityRequest()))
            {
                var _list = await _identity.GetListOfIssuesAsync();


                if (OrderBy == "Created")
                {
                    var _created = _list.OrderBy(x => x.CreatedDate);
                    return new OkObjectResult(_created);
                }
                if (OrderBy == "Edited")
                {
                    var _edited = _list.OrderBy(x => x.EditedDate);
                    return new OkObjectResult(_edited);
                }
                if (OrderBy == "Status")
                {
                    var _status = _list.OrderByDescending(x => x.CurrentStatus == Status);
                    return new OkObjectResult(_status);
                }
                if (OrderBy == "Customer")
                {
                    var _customer = _list.OrderByDescending(x => x.CustomerName == Customer);
                    return new OkObjectResult(_customer);
                }

                return new BadRequestObjectResult("No Valid Search Paramaters found try   Customer  Or  Status  Or  Created  Or  Edited");
            }
            return new UnauthorizedResult();

        }


        [HttpPost("issuecreate")]
        public async Task<IActionResult> IssueCreateAsync([FromBody] CreateIssueModel createIssueModel)
        {
            if (_identity.ValidateAccessRights(IdentityRequest()))
            {
                if (await _identity.CreateIssueAsync(createIssueModel))
                {
                    return new OkResult();
                }
                return new BadRequestResult();
            }
            return new UnauthorizedResult();

        }



        
        [HttpPut("issueupdate")]
        public async Task<IActionResult> IssueUpdateAsync([FromBody] UpdateIssueModel updateIssueModel)
        {
            if (_identity.ValidateAccessRights(IdentityRequest()))
            {
                if (await _identity.UpdateIssueAsync(updateIssueModel))
                {
                    return new OkResult();
                }
                return new BadRequestResult();
            }
            return new UnauthorizedResult();

        }
    }


}
