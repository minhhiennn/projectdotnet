using Model.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao.User
{
    public class UserInformation
    {
        private ProjectDotNetDbContext DbContext = null;
        public UserInformation()
        {
            DbContext = new ProjectDotNetDbContext();
        }
        public int InsertUserInfo(int user_id)
        {
            Framework.User_Information user_Information = new User_Information();
            user_Information.id_User = user_id;
            user_Information.Phone = null;
            user_Information.User_address = null;
            user_Information.Date_Of_Birth = null;
            DbContext.User_Information.Add(user_Information);
            DbContext.SaveChanges();
            return DbContext.User_Information.Max(x => x.id);
        }
        //lấy UserInfomation bằng User Id
        public Framework.User_Information getUserInfoByUserId(int userId)
        {
            return DbContext.User_Information.SingleOrDefault(x => x.id_User == userId);
        }
        //chuyển đổi DateTime to string để in ra View
        public string convertDate(string Date)
        {
            string date = Date.Split(' ')[0];
            //yyyy-mm-dd
            string[] dateSplit = date.Split('/');
            string month = dateSplit[0];
            string day = dateSplit[1];
            string year = dateSplit[2];
            if (day.Length < 2)
            {
                day = 0 + day;
            }
            if (month.Length < 2)
            {
                month = 0 + month;
            }
            string result = year + "-" + month + "-" + day;
            return result;
        }
        // chuyển đổi string qua chuẩn format string => datetime
        // dateofbirth => 2000-09-28
        // 09/28/2000 (không cần số 0 cũng dc)
        public string convertStringDateTime(string date)
        {
            string[] dateSplit = date.Split('-');
            string year = dateSplit[0];
            string month = dateSplit[1];
            string day = dateSplit[2];
            return month + "/" + day + "/" + year;
        }
        //Update UserInformation bằng UserId
        public bool Update(int currentUserId, string phone, string address, string dateofbirth)
        {
            try
            {
                var UserInfo = DbContext.User_Information.SingleOrDefault(x => x.id_User == currentUserId);
                UserInfo.Phone = phone;
                UserInfo.User_address = address;
                UserInfo.Date_Of_Birth = DateTime.Parse(dateofbirth);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateWithImg(int currentUserId, string phone, string address, string dateofbirth,string imgUrl)
        {
            try
            {
                var UserInfo = DbContext.User_Information.SingleOrDefault(x => x.id_User == currentUserId);
                UserInfo.Phone = phone;
                UserInfo.User_address = address;
                UserInfo.Date_Of_Birth = DateTime.Parse(dateofbirth);
                UserInfo.imgUrl = imgUrl;
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
