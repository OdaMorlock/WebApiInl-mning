using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_InlämningAttempt4.Models.Model
{
    public class ListIssuesModel
    {
        public long IssuesId { get; set; }
        public string CustomerName { get; set; }
        public string IssueUser { get; set; }
        public string CurrentStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EditedDate { get; set; }



    }
}
