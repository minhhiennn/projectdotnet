using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Framework;
using Model.Dao.User;
using ProjectDotNet.Models;
namespace ProjectDotNet.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Models.UsersModel users)
        {
            var email = users.usersLogin.Email;
            var password = users.usersLogin.PassWord;

            var dao = new UserLoginDao();
            var dao2 = new UserDao();
            var dao3 = new CartDao();
            var result = dao.Login(email, password);
            if (result.Equals("Success"))
            {
                var IdCurrentUser = dao2.findUserWithEmailAndPassword(email, password).id;
                var role = dao2.findUserWithEmailAndPassword(email, password).role;
                var currentUser = new UserLoginSession(IdCurrentUser, email, password,role);
                Session.Add("currentUser", currentUser);
                if (dao3.checkCartUser(IdCurrentUser) == false)
                {
                    dao3.createCart(IdCurrentUser);
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", Model.ErrorList.LOGIN_WRONGPASS);
            }
            return View("Login");
        }
        public ActionResult Logout()
        {
            Session["currentUser"] = null;
            return RedirectToAction("Login", "Login");
        }
        [HttpPost]
        public ActionResult Register(Models.UsersModel users)
        {
            var name = users.usersRegister.UserName;
            var email = users.usersRegister.Email;
            var password = users.usersRegister.Password;
            bool findUser = new UserDao().findUserWithEmail(email);
            if (findUser == true)
            {
                ModelState.AddModelError("", Model.ErrorList.EMAILINVALID);
                return View("Login");
            }
            else
            {
                var dao = new UserRegisterDao();
                var dao2 = new UserInformation();
                var user_id = dao.InsertUser(name, email, password);
                var result = dao2.InsertUserInfo(user_id);
                return RedirectToAction("Index", "Home");
            }
        }
    }
}