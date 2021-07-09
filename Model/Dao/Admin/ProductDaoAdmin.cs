using Model.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao.Admin
{
    public class ProductDaoAdmin
    {
        private ProjectDotNetDbContext DbContext = null;
        public ProductDaoAdmin()
        {
            DbContext = new ProjectDotNetDbContext();
        }

        // lấy product bằng id
        public Framework.Product getProductById(int id)
        {
            return DbContext.Products.SingleOrDefault(x => x.id == id);
        }

        // lấy all product ko phân trang
        public IEnumerable<Framework.Product> getAllProduct()
        {
            return DbContext.Products;
        }
        public void InsertProduct(Framework.Product product)
        {
            DbContext.Products.Add(product);
            DbContext.SaveChanges();
        }

        public string getImgProduct(int productId)
        {
            return DbContext.Products.Find(productId).Product_Img;
        }
        public void deleleProduct(int productId)
        {
            var product = DbContext.Products.Find(productId);
            DbContext.Products.Remove(product);
            DbContext.SaveChanges();
        }

        //
        public void updateProduct(int productId,int idCompany, string productName,string productDescription, string productImg, double productPrice)
        {
            var product = DbContext.Products.Find(productId);
            product.id_Company = idCompany;
            product.Product_Name = productName;
            product.Product_Details = productDescription;
            product.Product_Img = productImg;
            product.Product_Price = productPrice;
            DbContext.SaveChanges();
        }
        public void updateProductwithoutImg(int productId, int idCompany, string productName, string productDescription,double productPrice)
        {
            var product = DbContext.Products.Find(productId);
            product.id_Company = idCompany;
            product.Product_Name = productName;
            product.Product_Details = productDescription;
            product.Product_Price = productPrice;
            DbContext.SaveChanges();
        }
    }
}
