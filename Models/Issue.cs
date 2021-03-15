using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi_InlämningAttempt4.Models
{

    public partial class Issue
    {
        public long Id { get; set; }
        public long IssueUserId { get; set; }
        public string IssueUserFirstName { get; set; }
        public string Customer { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EditedDate { get; set; }
        public bool ActivityStatus { get; set; }
        public bool FinishedStatus { get; set; }
        public string CurrentStatus { get; set; }

        public virtual IssueUser IssueUser { get; set; }

       
    }
}
