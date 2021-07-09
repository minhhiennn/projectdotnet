using Model.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao.Admin
{
    public class UserAdminDao
    {
        private ProjectDotNetDbContext DbContext = null;
        public UserAdminDao()
        {
            DbContext = new ProjectDotNetDbContext();
        }
        public IEnumerable<DTO.Admin.UserAdminDTO> getAllUser()
        {
            List<DTO.Admin.UserAdminDTO> listUser = new List<DTO.Admin.UserAdminDTO>();
            var Users = DbContext.Users;
            foreach (var user in Users)
            {
                var userInfo = DbContext.User_Information.SingleOrDefault(x => x.id_User == user.id);
                DTO.Admin.UserAdminDTO usser = new DTO.Admin.UserAdminDTO(user.id, user.username, user.email, user.password, user.role, userInfo.User_address, userInfo.Date_Of_Birth, userInfo.Phone, userInfo.imgUrl);
                listUser.Add(usser);
            }
            return listUser;
        }
        public DTO.Admin.UserAdminDTO getUser(int userId)
        {
            var user = DbContext.Users.SingleOrDefault(x => x.id == userId);
            var userInfo = DbContext.User_Information.SingleOrDefault(x => x.id_User == userId);
            var usser = new DTO.Admin.UserAdminDTO(user.id, user.username, user.email, user.password, user.role, userInfo.User_address, userInfo.Date_Of_Birth, userInfo.Phone, userInfo.imgUrl);
            return usser;
        }
        public void insertUser(Framework.User user)
        {
            DbContext.Users.Add(user);
            DbContext.SaveChanges();
        }

        public Framework.User getUserByEmailAndPassword(string email,string password)
        {
            var user = DbContext.Users.SingleOrDefault(x => x.email.Equals(email) && x.password.Equals(password));
            return user;
        }

        public void insertUserInfo(Framework.User_Information userInfo)
        {
            DbContext.User_Information.Add(userInfo);
            DbContext.SaveChanges();
        }
        public bool getUserWithEmail(string email)
        {
            var user = DbContext.Users.SingleOrDefault(x => x.email.Equals(email));
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void deleteUser(int userId)
        {
            var user = DbContext.Users.Find(userId);
            DbContext.Users.Remove(user);
            DbContext.SaveChanges();
        }
        public void deleteUserInfo(int userId)
        {
            var userInfo = DbContext.User_Information.SingleOrDefault(x=>x.id_User == userId);
            DbContext.User_Information.Remove(userInfo);
            DbContext.SaveChanges();
        }

        public string imgUrlUserInfo(int userId)
        {
            var userInfo = DbContext.User_Information.SingleOrDefault(x => x.id_User == userId);
            return userInfo.imgUrl;
        }
    }
}
