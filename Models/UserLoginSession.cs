using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectDotNet.Models
{
    [Serializable]
    public class UserLoginSession
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string role { get; set; }
        public UserLoginSession(int id,string email,string password,string role)
        {
            Id = id;
            Email = email;
            Password = password;
            this.role = role;
        }        
    }
}