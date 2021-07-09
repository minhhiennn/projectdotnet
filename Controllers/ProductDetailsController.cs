using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao.User;
using Model.DTO.User;
namespace ProjectDotNet.Controllers
{
    public class ProductDetailsController : Controller
    {
        // GET: ProductDetail
        public ActionResult ProductDetail(int id)
        {
            var product = new ProductDao().getProductById(id);
            var company = new CompanyDao().GetCompanyById(product.id_Company);
            var model = new ProductInfoDTO(product,company);
            return View(model);
        }
    }
}