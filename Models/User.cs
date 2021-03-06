using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

#nullable disable

namespace WebApi_InlämningAttempt4.Models
{
    public partial class User
    {
        public User()
        {
            IssueUsers = new HashSet<IssueUser>();
        }

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }


        public void CreatePasswordHash(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            }
        }

        public bool ValidatePasswordHash(string Passowrd)
        {
            using (var hmac = new HMACSHA512(PasswordSalt))
            {
                var computerHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(Passowrd));
                for (int i = 0; i < computerHash.Length; i++)
                {
                    if (computerHash[i] != PasswordHash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public virtual ICollection<IssueUser> IssueUsers { get; set; }

       
    }
}
