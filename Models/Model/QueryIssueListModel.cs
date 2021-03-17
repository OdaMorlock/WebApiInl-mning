using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_InlämningAttempt4.Models.Model
{
    public class QueryIssueListModel
    {
        public QueryIssueListModel()
        {

        }

        public QueryIssueListModel(string customerName, string status, DateTime created, DateTime edited)
        {
            CustomerName = customerName;
            Status = status;
            Created = created;
            Edited = edited;
        }

        public string CustomerName { get; set; }
        public string Status { get; set; }
        public DateTime  Created { get; set; }
        public DateTime Edited { get; set; }
    }
}
