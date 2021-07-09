using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO.User
{
    public class UserInformation
    {
        public string UserAddress { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Phone { get; set; }
    }
}
