using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_InlämningAttempt4.Models
{
    public class SessionTokens
    {
        public long UserId { get; set; }
       
        public string AccessToken { get; set; }

        public virtual User User { get; set; }
    }
}
