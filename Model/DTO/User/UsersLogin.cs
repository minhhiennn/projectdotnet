using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Model.DTO.User
{
    public class UsersLogin
    {
       
        public string Email { get; set; }
        
        public string PassWord { get; set; }
    }
}
