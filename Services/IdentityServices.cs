using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<ListIssuesModel>> GetListOfIssuesByCustomerName(string CustomerName)
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

        public async Task<IEnumerable<ListIssuesModel>> GetListOfIssuesByStatus(string Status)
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

        public Task<IEnumerable<ListIssuesModel>> GetListOfIssuesByDateCreated(DateTime Created)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ListIssuesModel>> GetListOfIssuesByDateEdited(DateTime Edited)
        {
            throw new NotImplementedException();
        }


        //Orderby och OrderByDecending(x => x. Created)




    }
}
    