using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_InlämningAttempt4.Models.Model
{
    public class SignInResponseModel
    {
        public bool Succeded { get; set; }
        public SignInResponseResult Result { get; set; }

    }
    public class SignInResponseResult
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }
    }
}
