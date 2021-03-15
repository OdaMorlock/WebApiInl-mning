using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_InlämningAttempt4.Models.Model
{
    public class CreateIssueModel
    {
        public string CustomerName { get; set; }
        public int IssueUserId { get; set; }
        public string  IssueUserFirstName { get; set; }
        public bool ActiveStatus { get; set; }
        public bool FinishedStatus { get; set; }

        public bool CurrentActiveStatus (bool activeStatus)
        {
            if (activeStatus == true)
            {
                return true;
            }

            return false;
        }

        public DateTime CreatedDateTime()
        {
           DateTime createdDate = DateTime.Now;
            return createdDate;
        }

        public DateTime EditedDateTime()
        {
           DateTime editedDate = DateTime.Now;
            return editedDate;
        }

        public string CurrentStatusDecider(bool activeStatus, bool finishedStatus)
        {
            string currentStatus = "Went Wrong";


            if (finishedStatus == true)
            {
                currentStatus = "Finished";
                return currentStatus;
            }
            else
            {
                if (activeStatus == true && finishedStatus == false)
                {
                    currentStatus = "Active";
                    return currentStatus;
                }
                else if (activeStatus == false && finishedStatus == false)
                {
                    currentStatus = "InActive";
                    return currentStatus;
                }
            }

            

            return currentStatus;

        }
    }
}
