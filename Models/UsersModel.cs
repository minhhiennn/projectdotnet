using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model.DTO.User;

namespace ProjectDotNet.Models
{
    public class UsersModel
    {
        public UsersLogin usersLogin { get; set; }
        public UsersRegister usersRegister { get; set; }
    }
}