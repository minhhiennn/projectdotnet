using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO.Admin
{
    public class UserAdminDTO
    {
        public int id_User { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public string User_address { get; set; }
        public DateTime? DOB { get; set; }
        public string Phone { get; set; }
        public string imgUrl { get; set; }
        public UserAdminDTO(int id_User,string userName,string email,string password,string role,string User_address,DateTime? DOB,string Phone,string imgUrl)
        {
            this.id_User = id_User;
            this.userName = userName;
            this.email = email;
            this.password = password;
            this.role = role;
            this.User_address = User_address;
            this.DOB = DOB;
            this.Phone = Phone;
            this.imgUrl = imgUrl;
        }
    }
}
