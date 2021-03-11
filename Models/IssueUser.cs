using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi_InlämningAttempt4.Models
{
    public partial class IssueUser
    {
        public IssueUser()
        {
            Issues = new HashSet<Issue>();
        }

        public long Id { get; set; }
        public long UserId { get; set; }
        public string UserFirstName { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Issue> Issues { get; set; }
    }
}
