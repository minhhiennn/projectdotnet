using Model.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao.User
{
    public class UserRegisterDao
    {
        private ProjectDotNetDbContext DbContext = null;
        public UserRegisterDao()
        {
            DbContext = new ProjectDotNetDbContext();
        }
        public int InsertUser(string username, string email, string password)
        {
            Framework.User user = new Framework.User();
            user.username = username;
            user.email = email;
            user.password = password;
            user.role = "user";
            DbContext.Users.Add(user);
            DbContext.SaveChanges();             
            return DbContext.Users.Max(x => x.id);
        }
    }
}
