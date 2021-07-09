using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao.Admin;
namespace ProjectDotNet.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Admin/Product
        public ActionResult ProductIndex()
        {
            var dao = new ProductDaoAdmin();
            var model = dao.getAllProduct();
            
            return View(model);
        }

        public ActionResult DeleteMultiProduct(string deleteIds)
        {
            string[] allProductIdS = deleteIds.Split(',');
            var dao = new ProductDaoAdmin();
            foreach (string productIdS in allProductIdS)
            {
                int productId = int.Parse(productIdS);
                string filePath = Server.MapPath(dao.getImgProduct(productId));
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                dao.deleleProduct(productId);
            }
            return RedirectToAction("ProductIndex", "Product");
        }
        public ActionResult CreateProduct()
        {
            var dao = new CompanyDaoAdmin();
            var companyName = dao.getAllCompany();
            // Tạo SelectList
            SelectList cateList = new SelectList(companyName, "id", "company_Name");
            // Set vào ViewBag
            ViewBag.CategoryList = cateList;
            return View();
        }
        [HttpPost]
        public ActionResult CreateProduct(FormCollection form, HttpPostedFileBase img)
        {
            if (img != null && img.ContentLength > 0)
            {
                string productName = form.Get("productName").ToString();
                double productPrice = Double.Parse(form.Get("productPrice").ToString());
                string productDescription = form.Get("productDescription").ToString();
                int idCompany = int.Parse(form.Get("CompanyName").ToString());
                try
                {
                    string fileName = Path.GetFileName(img.FileName);
                    // save hình ảnh vào file data
                    string path = Path.Combine(Server.MapPath("/Data/ProductImages/"), fileName);
                    img.SaveAs(path);


                    // lưu xuống database tất cả thông tin                   
                    string productImg = "/Data/ProductImages/" + fileName;
                    Model.Framework.Product product = new Model.Framework.Product();
                    // lưu product
                    product.id_Company = idCompany;
                    product.Product_Name = productName;
                    product.Product_Details = productDescription;
                    product.Product_Img = productImg;
                    product.Product_Price = productPrice;

                    var dao = new ProductDaoAdmin();
                    dao.InsertProduct(product);
                    return RedirectToAction("ProductIndex", "Product");
                }
                catch (Exception ex)
                {
                    var dao = new CompanyDaoAdmin();
                    var companyName = dao.getAllCompany();
                    // Tạo SelectList
                    SelectList cateList = new SelectList(companyName, "id", "company_Name");
                    // Set vào ViewBag
                    ViewBag.CategoryList = cateList;
                    ViewBag.Err = ex.Message;
                    return View();
                }
            }
            else
            {
                var dao = new CompanyDaoAdmin();
                var companyName = dao.getAllCompany();
                // Tạo SelectList
                SelectList cateList = new SelectList(companyName, "id", "company_Name");
                // Set vào ViewBag
                ViewBag.CategoryList = cateList;
                ViewBag.Err = "Hình ảnh ko dc để null";
                return View();
            }
           
        }

        public ActionResult DeleteProduct(int productId)
        {
            var dao = new ProductDaoAdmin();
            string filePath = Server.MapPath(dao.getImgProduct(productId));
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            dao.deleleProduct(productId);
            return RedirectToAction("ProductIndex", "Product");
        }
        public ActionResult UpdateProduct(int productId)
        {
            var dao2 = new CompanyDaoAdmin();
            var company = dao2.getAllCompany();
            ViewBag.Company = company;
            var dao = new ProductDaoAdmin();
            var model = dao.getProductById(productId);
            return View(model);
        }
        [HttpPost]        
        public ActionResult UpdateProduct(FormCollection form, HttpPostedFileBase img)
        {
            var dao = new ProductDaoAdmin();
            int productId = int.Parse(form.Get("productId").ToString());
            string productName = form.Get("productName").ToString();
            double productPrice = Double.Parse(form.Get("productPrice").ToString());
            string productDescription = form.Get("productDescription").ToString();
            int idCompany = int.Parse(form.Get("CompanyName").ToString());
            if (img != null && img.ContentLength > 0)
            {
                try
                {
                    // delete tấm hình cũ
                    string filePath = Server.MapPath(dao.getImgProduct(productId));
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }

                    string fileName = Path.GetFileName(img.FileName);
                    // save hình ảnh vào file data
                    string path = Path.Combine(Server.MapPath("/Data/ProductImages/"), fileName);
                    img.SaveAs(path);
                    // lưu xuống database tất cả thông tin                   
                    string productImg = "/Data/ProductImages/" + fileName;                   
                    // lưu product
                    dao.updateProduct(productId,idCompany,productName,productDescription,productImg,productPrice);
                    return RedirectToAction("ProductIndex", "Product");
                }
                catch (Exception ex)
                {
                    var dao2 = new CompanyDaoAdmin();
                    var company = dao2.getAllCompany();
                    ViewBag.Company = company;
                    var daoo = new ProductDaoAdmin();
                    var model = daoo.getProductById(productId);
                    ViewBag.Err = ex.Message;
                    return View(model);
                }
            }
            else
            {
                dao.updateProductwithoutImg(productId, idCompany, productName, productDescription, productPrice);
                return RedirectToAction("ProductIndex", "Product");
            }
        }
    }
}