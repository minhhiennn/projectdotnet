using Model.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao.User
{
    public class UserDao
    {
        private ProjectDotNetDbContext DbContext = null;
        public UserDao()
        {
            DbContext = new ProjectDotNetDbContext();
        }
        // tìm user bằng email
        public bool findUserWithEmail(string email)
        {
            var user = DbContext.Users.SingleOrDefault(x => x.email.Equals(email));
            if (user != null)
            {
                return true;
            }
            return false;
        }
        public Framework.User findUserWithEmailAndPassword(string email,string password)
        {
            return DbContext.Users.SingleOrDefault(x=>x.email.Equals(email) && x.password.Equals(password));
        }        
    }
}
