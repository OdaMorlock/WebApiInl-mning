using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApi_InlämningAttempt4.Data;
using WebApi_InlämningAttempt4.Models;
using WebApi_InlämningAttempt4.Models.Model;

namespace WebApi_InlämningAttempt4.Services
{
    public class IdentityServices : IIdentityServices
    {
        private readonly SqlDbContext _context;
        private IConfiguration _configuration { get; }

        public IdentityServices(SqlDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

       
        public async Task<bool> CreateUserAsync(SignUpModel signUpModel)
        {
            if (! _context.Users.Any(user => user.Email == signUpModel.Email))
            {
                try
                {
                    var user = new User()
                    {
                        FirstName = signUpModel.FirstName,
                        LastName = signUpModel.LastName,
                        Email = signUpModel.Email
                    };

                    user.CreatePasswordHash(signUpModel.Password);
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();

                    if (signUpModel.IssueUser == true)
                    {
                        var issueUser = new IssueUser()
                        {
                            UserId = user.Id,
                            UserFirstName = user.FirstName
                        };
                        _context.IssueUsers.Add(issueUser);
                    }
                    await _context.SaveChangesAsync();

                    return true;


                }
                catch 
                {

                    
                }
            }
            return false;
        }

        public async Task<SignInResponseModel> SignInAsync(string Email, string Password)
        {

            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == Email);

                if (user != null)
                {
                    try
                    {
                        if (user.ValidatePasswordHash(Password))
                        {
                            var tokenHandler = new JwtSecurityTokenHandler();
                            var _secretKey = Encoding.UTF8.GetBytes(_configuration.GetSection("SecretKey").Value);

                            var tokenDescriptor = new SecurityTokenDescriptor
                            {
                                Subject = new ClaimsIdentity(new Claim[] { new Claim("UserId", user.Id.ToString()) }),
                                Expires = DateTime.Now.AddMinutes(5),
                                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_secretKey), SecurityAlgorithms.HmacSha512Signature)
                            };

                            var _accessToken = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

                            _context.SessionTokens.Add(new SessionToken { UserId = user.Id, AccessToken = _accessToken });

                            await _context.SaveChangesAsync();

                            return new SignInResponseModel
                            {
                                Succeded = true,
                                Result = new SignInResponseResult
                                {
                                    Id = user.Id,
                                    Email = user.Email,
                                    AccessToken = _accessToken

                                }
                            };

                        }
                    }
                    catch
                    {


                    }
                }

            }
            catch 
            {

                
            }

            return new SignInResponseModel { Succeded = false };
            

        }


        public async Task<bool> CreateIssueAsync(CreateIssueModel createIssueModel)
        {
            if (! _context.Issues.Any(issue => issue.Customer == createIssueModel.CustomerName))
            {


                try
                {
                    var issue = new Issue()
                    {
                        IssueUserId = createIssueModel.IssueUserId,
                        IssueUserFirstName = createIssueModel.IssueUserFirstName,
                        Customer = createIssueModel.CustomerName,
                        ActivityStatus = createIssueModel.ActiveStatus,
                        FinishedStatus = createIssueModel.FinishedStatus,
                        CurrentStatus = createIssueModel.CurrentStatusDecider(createIssueModel.ActiveStatus, createIssueModel.FinishedStatus),
                        CreatedDate = createIssueModel.CreatedDateTime(),
                        EditedDate = createIssueModel.EditedDateTime()

                    };

                    _context.Issues.Add(issue);
                    await _context.SaveChangesAsync();

                    return true;


                }
                catch 
                {

                    
                }

            }

            return false;
            
        }

        public async Task<bool> UpdateIssueAsync(UpdateIssueModel updateIssueModel)
        {

            if (_context.Issues.Any(issue => issue.Id == updateIssueModel.IssueId))
            {
                try
                {

                    //https://stackoverflow.com/questions/36144178/asp-net-web-api-controller-update-row

                    var updateIssue = _context.Issues.FirstOrDefault(x => x.Id == updateIssueModel.IssueId);

                    if (updateIssue != null)
                    {
                        updateIssue.IssueUserId = updateIssueModel.IssueUserId;
                        updateIssue.IssueUserFirstName = updateIssueModel.IssueUserFirstName;
                        updateIssue.Customer = updateIssueModel.CustomerName;
                        updateIssue.ActivityStatus = updateIssueModel.ActiveStatus;
                        updateIssue.FinishedStatus = updateIssueModel.FinishedStatus;
                        updateIssue.CurrentStatus = updateIssueModel.CurrentStatusDecider(updateIssueModel.ActiveStatus, updateIssueModel.FinishedStatus);
                        updateIssue.EditedDate = updateIssueModel.EditedDateTime();
                        await _context.SaveChangesAsync();

                    }


                    return true;


                }
                catch
                {


                }

            }

            return false;
        }

        public async Task<IEnumerable<ListIssuesModel>> GetListOfIssuesAsync()
        {
            var lists = new List<ListIssuesModel>();


                foreach (var issue in await _context.Issues.ToListAsync())
                {
                    lists.Add(new ListIssuesModel { 
                        IssuesId = issue.Id,
                        CustomerName = issue.Customer, 
                        IssueUser = issue.IssueUserFirstName, 
                        CurrentStatus = issue.CurrentStatus, 
                        CreatedDate = issue.CreatedDate, 
                        EditedDate = issue.EditedDate });
                }

            return lists;
        }

        public async Task<IEnumerable<ListIssuesModel>> GetListOfIssuesByCustomerNameAsync(string CustomerName)
        {
            var lists = new List<ListIssuesModel>();
            


            foreach (var issue in await _context.Issues.ToListAsync())
            {
                lists.Add(new ListIssuesModel
                {
                    IssuesId = issue.Id,
                    CustomerName = issue.Customer,
                    IssueUser = issue.IssueUserFirstName,
                    CurrentStatus = issue.CurrentStatus,
                    CreatedDate = issue.CreatedDate,
                    EditedDate = issue.EditedDate
                });
            }

            var result = lists.FindAll(x => x.CustomerName == CustomerName);
            return result;




        }

        public async Task<IEnumerable<ListIssuesModel>> GetListOfIssuesByStatusAsync(string Status)
        {
            var lists = new List<ListIssuesModel>();


            foreach (var issue in await _context.Issues.ToListAsync())
            {
                lists.Add(new ListIssuesModel
                {
                    IssuesId = issue.Id,
                    CustomerName = issue.Customer,
                    IssueUser = issue.IssueUserFirstName,
                    CurrentStatus = issue.CurrentStatus,
                    CreatedDate = issue.CreatedDate,
                    EditedDate = issue.EditedDate
                });
            }

           var result =  lists.FindAll(x => x.CurrentStatus == Status);

          

           return result;

        }

        public async Task<IEnumerable<ListIssuesModel>> GetListOfIssuesByDateCreatedAsync(DateTime Created)
        {
            var lists = new List<ListIssuesModel>();



            foreach (var issue in await _context.Issues.ToListAsync())
            {
                lists.Add(new ListIssuesModel
                {
                    IssuesId = issue.Id,
                    CustomerName = issue.Customer,
                    IssueUser = issue.IssueUserFirstName,
                    CurrentStatus = issue.CurrentStatus,
                    CreatedDate = issue.CreatedDate,
                    EditedDate = issue.EditedDate
                });
            }
            var _date = Created.Date;
            var _hour = Created.Hour;
            var result = lists.FindAll(x => x.EditedDate.Date == _date);
            return result;
        }

        public async Task<IEnumerable<ListIssuesModel>> GetListOfIssuesByDateEditedAsync(DateTime Edited)
        {
            var lists = new List<ListIssuesModel>();



            foreach (var issue in await _context.Issues.ToListAsync())
            {
                lists.Add(new ListIssuesModel
                {
                    IssuesId = issue.Id,
                    CustomerName = issue.Customer,
                    IssueUser = issue.IssueUserFirstName,
                    CurrentStatus = issue.CurrentStatus,
                    CreatedDate = issue.CreatedDate,
                    EditedDate = issue.EditedDate
                });
            }
            var _date = Edited.Date;
            var _hour = Edited.Hour;
            var result = lists.FindAll(x => x.EditedDate.Date == _date);
            return result;

        }




        //Orderby och OrderByDecending(x => x. Created)




    }
}
    