using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao.User;
using Model.DTO.User;
using Model.Framework;

namespace ProjectDotNet.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Cart()
        {
            var currentUser = (ProjectDotNet.Models.UserLoginSession)Session["currentUser"];
            if (currentUser == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                var dao = new CartDao();
                var model = dao.getCartItemDTOById(currentUser.Id);
                ViewBag.totalCart = dao.totalCart(currentUser.Id);
                return View(model);
            }
        }
        public ActionResult AddToCart(int productId, int quantity)
        {
            var currentUser = (ProjectDotNet.Models.UserLoginSession)Session["currentUser"];
            if (currentUser == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                var dao = new CartDao();
                dao.addProduct(currentUser.Id, productId, quantity);
                return RedirectToAction("Cart", "Cart");
            }
        }
        public ActionResult DeleteItem(int productId)
        {
            var currentUser = (ProjectDotNet.Models.UserLoginSession)Session["currentUser"];
            var dao = new CartDao();
            dao.deleteProduct(currentUser.Id, productId);
            return RedirectToAction("Cart", "Cart");
        }

        public ActionResult decreaseAndIncrease(int productId, string action2)
        {
            var currentUser = (ProjectDotNet.Models.UserLoginSession)Session["currentUser"];
            var dao = new CartDao();
            var x = action2;
            dao.decreaseOrIncrease(currentUser.Id, productId, action2);
            return RedirectToAction("Cart", "Cart");
        }

        public ActionResult changeProductQuantity(int productId,int quantity)
        {
            var currentUser = (ProjectDotNet.Models.UserLoginSession)Session["currentUser"];
            var dao = new CartDao();
            dao.changeProductQuantity(currentUser.Id,productId,quantity);
            return RedirectToAction("Cart", "Cart");
        }
    }
}