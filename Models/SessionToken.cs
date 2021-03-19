using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace WebApi_InlämningAttempt4.Models
{
    public partial class SessionToken
    {
        public long UserId { get; set; }

        public string AccessToken { get; set; }

        public virtual User User { get; set; }
    }
}
