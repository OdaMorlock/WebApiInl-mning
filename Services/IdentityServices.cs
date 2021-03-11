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
    }
}
