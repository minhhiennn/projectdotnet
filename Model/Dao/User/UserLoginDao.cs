using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Framework;

namespace Model.Dao.User
{
    public class UserLoginDao
    {
        private ProjectDotNetDbContext DbContext = null;
        public UserLoginDao()
        {
            DbContext = new ProjectDotNetDbContext();
        }
        public string Login(string email,string password)
        {
            var result = DbContext.Users.SingleOrDefault(x => x.email == email && x.password == password);
            if (result == null)
            {
                return "inCorrect";
            }
            else
            {
                return "Success";
            }
        }
    }
}
