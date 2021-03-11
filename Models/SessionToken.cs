using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi_InlämningAttempt4.Models
{
    public partial class SessionToken
    {
        public long UserId { get; set; }
        public byte[] AccessToken { get; set; }

        public virtual User User { get; set; }
    }
}
