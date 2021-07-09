using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao.User;

namespace ProjectDotNet.Controllers
{
    public class ShopFindByPriceController : Controller
    {
        // GET: ShopFindByPrice

        // /ShopFindByPrice/FindProduct?price=@(ViewBag.So1+"%2C"+ViewBag.So2)&page=2
        public ActionResult FindProduct(string price, int? page)
        {
            if (page == null)
            {
                int priceLow = int.Parse(price.Split(',')[0]);
                int priceHigh = int.Parse(price.Split(',')[1]);
                var dao = new ProductDao();
                int start = 9 * 0;
                int pageSize = 9;
                var listProduct = dao.getProductByIdAndPrice(start, pageSize,priceLow,priceHigh);
                int pageSplit = dao.getPageForPrice(priceLow,priceHigh);
                ViewBag.lowPrice = priceLow;
                ViewBag.highPrice = priceHigh;
                ViewBag.currentPage = 1;
                ViewBag.pageSplit = pageSplit;
                return View(listProduct);
            }
            else
            {
                int priceLow = int.Parse(price.Split(',')[0]);
                int priceHigh = int.Parse(price.Split(',')[1]);
                var dao = new ProductDao();
                int start = 9 * ((int)page - 1);
                int pageSize = 9;
                var listProduct = dao.getProductByIdAndPrice(start, pageSize, priceLow, priceHigh);
                int pageSplit = dao.getPageForPrice(priceLow, priceHigh);
                ViewBag.lowPrice = priceLow;
                ViewBag.highPrice = priceHigh;
                ViewBag.currentPage = (int)page;
                ViewBag.pageSplit = pageSplit;
                return View(listProduct);
            }
        }
    }
}