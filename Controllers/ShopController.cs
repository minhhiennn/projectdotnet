using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao.User;
using Model.Framework;

namespace ProjectDotNet.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        public ActionResult Shop(int page)
        {
            var dao = new ProductDao();
            int start =9 * (page-1);
            int pageSize = 9;
            var listProduct = dao.getAllListProduct(start, pageSize);
            int pageSplit = dao.getPage();
            ViewBag.currentPage = page;
            ViewBag.pageSplit = pageSplit;
            return View(listProduct);
        }
        public ActionResult ShopSearchByName(string search,int page)
        {
            var dao = new ProductDao();
            int start = 9 * (page - 1);
            int pageSize = 9;
            var listProduct = dao.getProductByIdAndName(start, pageSize, search);
            int pageSplit = dao.getPageForName(search);
            ViewBag.stringSearch = search;
            ViewBag.currentPage = page;
            ViewBag.pageSplit = pageSplit;
            return View(listProduct);
        }
    }
}