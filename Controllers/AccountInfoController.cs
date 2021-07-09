using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao.User;
using System.IO;

namespace ProjectDotNet.Controllers
{
    public class AccountInfoController : Controller
    {
        // GET: AccountInfo
        public ActionResult AccountInformation()
        {
            var currentUser = (ProjectDotNet.Models.UserLoginSession)Session["currentUser"];
            var TenDangNhap = new UserDao().findUserWithEmailAndPassword(currentUser.Email, currentUser.Password).username;
            var dao = new UserInformation();
            var UserInfo = dao.getUserInfoByUserId(currentUser.Id);
            ViewBag.TenDangNhap = TenDangNhap;
            ViewBag.Email = currentUser.Email;
            string date = null;
            if (UserInfo.Date_Of_Birth != null)
            {
                date = dao.convertDate(UserInfo.Date_Of_Birth.ToString());
            }            
            ViewBag.DOB = date;
            ViewBag.Phone = UserInfo.Phone;
            ViewBag.Address = UserInfo.User_address;
            ViewBag.ImgUrl = UserInfo.imgUrl;
            return View();
        }
        [HttpPost]
        public ActionResult SaveUserInfo(FormCollection form, HttpPostedFileBase img)
        {
            var currentUser = (ProjectDotNet.Models.UserLoginSession)Session["currentUser"];
            var currentUserId = currentUser.Id;
            var dao = new UserInformation();
            string phone = form.Get("phone").ToString();
            string address = form.Get("address").ToString();
            string dateofbirth = dao.convertStringDateTime(form.Get("dateofbirth").ToString());
            if (img != null && img.ContentLength > 0)
            {
                try
                {
                    string fileName = Path.GetFileName(img.FileName);
                    // save hình ảnh vào file data
                    string path = Path.Combine(Server.MapPath("/Data/Images/"), fileName);
                    img.SaveAs(path);


                    // lưu xuống database tất cả thông tin                   
                    string urlImg = "/Data/Images/"+ fileName;
                    bool result = dao.UpdateWithImg(currentUserId, phone, address, dateofbirth, urlImg);                   
                    return RedirectToAction("AccountInformation", "AccountInfo");             
                }
                catch(Exception ex)
                {
                    ViewBag.Message = "Err" + ex.Message.ToString();
                    return RedirectToAction("AccountInformation", "AccountInfo",ViewBag.Message);
                }
            }
            else
            {
                bool result = dao.Update(currentUserId, phone, address, dateofbirth);
                return RedirectToAction("AccountInformation", "AccountInfo");
            }
        }
    }
}