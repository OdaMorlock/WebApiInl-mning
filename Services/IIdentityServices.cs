using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_InlämningAttempt4.Models.Model;

namespace WebApi_InlämningAttempt4.Services
{
    public interface IIdentityServices
    {
        Task<bool> CreateUserAsync(SignUpModel signUpModel);

        Task<bool> CreateIssueAsync(CreateIssueModel createIssueModel);

        Task<bool> UpdateIssueAsync(UpdateIssueModel updateIssueModel);
        Task<IEnumerable<ListIssuesModel>> GetListOfIssuesAsync();
        Task<IEnumerable<ListIssuesModel>> GetListOfIssuesByCustomerName(string CustomerName);
        Task<IEnumerable<ListIssuesModel>> GetListOfIssuesByStatus(string Status);
        Task<IEnumerable<ListIssuesModel>> GetListOfIssuesByDateCreated(DateTime Created);
        Task<IEnumerable<ListIssuesModel>> GetListOfIssuesByDateEdited(DateTime Edited);

    }
}
