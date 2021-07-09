using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao.Admin;
using Model.Dao.User;

namespace ProjectDotNet.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User
        public ActionResult UserIndex()
        {
            var dao = new UserAdminDao();
            var model = dao.getAllUser();
            return View(model);
        }
        public ActionResult UserDetail(int userId)
        {
            var dao = new UserAdminDao();
            var model = dao.getUser(userId);
            return View(model);
        }
        public ActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateUser(FormCollection form, HttpPostedFileBase img)
        {
            var dao = new UserInformation();
            var dao2 = new UserAdminDao();
            string username = form.Get("username").ToString();
            string email = form.Get("email").ToString();
            string password = form.Get("password").ToString();
            string role = form.Get("role").ToString();
            string address = form.Get("address").ToString();
            string DOB = dao.convertStringDateTime(form.Get("DOB").ToString());
            string phone = form.Get("Phone").ToString();
            if (dao2.getUserWithEmail(email) == false)
            {
                if (img != null && img.ContentLength > 0)
                {
                    try
                    {
                        // lưu xuống database thông tin user
                        Model.Framework.User user = new Model.Framework.User();
                        user.username = username;
                        user.email = email;
                        user.password = password;
                        user.role = role;
                        dao2.insertUser(user);

                        int userId = dao2.getUserByEmailAndPassword(email, password).id;

                        string fileName = Path.GetFileName(img.FileName);
                        // save hình ảnh vào file data
                        string path = Path.Combine(Server.MapPath("/Data/Images/"), fileName);
                        img.SaveAs(path);


                        // lưu xuống database thông tin User                   
                        string urlImg = "/Data/Images/" + fileName;
                        Model.Framework.User_Information userInfo = new Model.Framework.User_Information();
                        userInfo.id_User = userId;
                        userInfo.User_address = address;
                        userInfo.Date_Of_Birth = DateTime.Parse(DOB);
                        userInfo.Phone = phone;
                        userInfo.imgUrl = urlImg;
                        dao2.insertUserInfo(userInfo);
                        return RedirectToAction("UserIndex", "User");
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "Err" + ex.Message.ToString();
                        return View();
                    }
                }
                else
                {
                    Model.Framework.User user = new Model.Framework.User();
                    user.username = username;
                    user.email = email;
                    user.password = password;
                    user.role = role;
                    dao2.insertUser(user);

                    int userId = dao2.getUserByEmailAndPassword(email, password).id;

                    Model.Framework.User_Information userInfo = new Model.Framework.User_Information();
                    userInfo.id_User = userId;
                    userInfo.User_address = address;
                    userInfo.Date_Of_Birth = DateTime.Parse(DOB);
                    userInfo.Phone = phone;
                    userInfo.imgUrl = "/Data/Images/menicon.jpg";
                    dao2.insertUserInfo(userInfo);
                    return RedirectToAction("UserIndex", "User");
                }
            }
            else
            {
                ViewBag.Err = "email đã tồn tại";
                return View();
            }
        }

        public ActionResult UpdateUser(int userId)
        {
            var dao = new UserAdminDao();
            var model = dao.getUser(userId);
            return View(model);
        }
        public ActionResult DeleteUser(int userId)
        {
            var dao = new UserAdminDao();
            var dao2 = new CartDaoAdmin();
            string imageUrl = dao.imgUrlUserInfo(userId);
            string filePath = Server.MapPath(imageUrl);
            if (System.IO.File.Exists(filePath) && !Path.GetFileName(filePath).Equals("menicon.jpg")) 
            {
                System.IO.File.Delete(filePath);
            }
            dao.deleteUserInfo(userId);
            dao2.deleteCartItem(userId);
            dao2.deleteCart(userId);
            dao.deleteUser(userId);

            return RedirectToAction("UserIndex", "User");
        }
        public ActionResult DeleteMultiUser(string deleteIds)
        {
            string[] allUserIdS = deleteIds.Split(',');
            var dao = new UserAdminDao();
            var dao2 = new CartDaoAdmin();
            foreach (string idUserS in allUserIdS)
            {
                int userId = int.Parse(idUserS);
                string imageUrl = dao.imgUrlUserInfo(userId);
                string filePath = Server.MapPath(imageUrl);
                if (System.IO.File.Exists(filePath) && !Path.GetFileName(filePath).Equals("menicon.jpg"))
                {
                    System.IO.File.Delete(filePath);
                }
                dao.deleteUserInfo(userId);
                dao2.deleteCartItem(userId);
                dao2.deleteCart(userId);
                dao.deleteUser(userId);
            }
            return RedirectToAction("UserIndex", "User");
        }
    }
}