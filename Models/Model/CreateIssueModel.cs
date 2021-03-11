using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_InlämningAttempt4.Models.Model
{
    public class CreateIssueModel
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public int IssueUserId { get; set; }
        public string  IssueUserFirstName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EditedDate { get; set; }
        public string CurrentStatus { get; set; }
        public bool ActiveStatus { get; set; }
        public bool FinishedStatus { get; set; }
    }
}
