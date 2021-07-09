using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public static class ErrorList

    {
        public static string EMAILINVALID = "Email này đã được sử dụng";
        public static string Wrong_Validation = "Dữ liệu nhập vào không đúng định dạng,vui lòng kiểm tra lại!";
        //Login
        public static string LOGIN_NULLINFO = "Tài Khoản hoặc mật khẩu không được để trống";
        public static string LOGIN_WRONGPASS = "Tài Khoản hoặc mật khẩu không chính xác";
        public static string LOGIN_LOCKED = "Tài Khoản đã bị khóa";
        //Register
        public static string REGISTER_ERROR = "Đăng kí tài khoản không thành công";
        //Profile
        public static string UPDATE_ERROR = "Cập nhật thông tin không thành công";
        public static string DELETE_ERROR = "Xóa không thông tin không thành công";
        //Edit Password
        public static string EDITPASSWORD_ERROR = "Cập nhật mật khẩu không thành công";
        public static string WRONGPASSWORD_ERROR = "Mật khẩu cũ không chính xác";
        public static string PASSWORDCONFIRM_ERROR = "Mật khẩu cũ và mật khẩu xác nhận không khớp";
    }
}
