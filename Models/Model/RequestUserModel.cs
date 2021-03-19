using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_InlämningAttempt4.Models.Model
{
    public class RequestUserModel
    {
        public long UserId { get; set; }
        public string AccessToken { get; set; }
    }
}
