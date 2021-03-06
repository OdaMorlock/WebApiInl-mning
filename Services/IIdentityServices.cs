using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_InlämningAttempt4.Models;
using WebApi_InlämningAttempt4.Models.Model;

namespace WebApi_InlämningAttempt4.Services
{
    public interface IIdentityServices
    {
        bool ValidateAccessRights(RequestUserModel requestUserModel);
        Task<bool> CreateUserAsync(SignUpModel signUpModel);

        Task<SignInResponseModel> SignInAsync(string Email, string Password);

        Task<IEnumerable<GetUsersModel>> GetListOfUsersAsync();
        Task<IEnumerable<GetIssueUserModel>> GetListOfIssueUserAsync();

        Task<bool> CreateIssueAsync(CreateIssueModel createIssueModel);

        Task<bool> UpdateIssueAsync(UpdateIssueModel updateIssueModel);
        Task<IEnumerable<ListIssuesModel>> GetListOfIssuesAsync();
        Task<IEnumerable<ListIssuesModel>> GetListOfIssuesByCustomerNameAsync(string CustomerName);
        Task<IEnumerable<ListIssuesModel>> GetListOfIssuesByStatusAsync(string Status);
        Task<IEnumerable<ListIssuesModel>> GetListOfIssuesByDateCreatedAsync(DateTime Created);
        Task<IEnumerable<ListIssuesModel>> GetListOfIssuesByDateEditedAsync(DateTime Edited);

    }
}
