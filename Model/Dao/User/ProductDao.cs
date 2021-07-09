using Model.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao.User
{
    public class ProductDao
    {
        private ProjectDotNetDbContext DbContext = null;
        public ProductDao()
        {
            DbContext = new ProjectDotNetDbContext();
        }

        // lấy product bằng id
        public Framework.Product getProductById(int id)
        {
            return DbContext.Products.SingleOrDefault(x => x.id == id);
        }


        //lấy all list product 
        public IEnumerable<Framework.Product> getAllListProduct(int start, int pageSize)
        {
            //skip(x).take(y) => lấy phần thử thứ id = x+1 đến = x+y
            return DbContext.Products.OrderBy(x => x.id).Skip(start).Take(pageSize);
        }
        // tính số page cho phần chia trang(không có kèm theo price)
        public int getPage()
        {
            int countAllProduct = DbContext.Products.Count();
            if (countAllProduct % 9 == 0)
            {
                return (countAllProduct / 9);
            }
            else
            {
                return (countAllProduct / 9) + 1;
            }
        }

        // lấy list product bằng id và kèm theo price
        public IEnumerable<Framework.Product> getProductByIdAndPrice(int start, int pageSize, int priceLow, int priceHigh)
        {
            return DbContext.Products.Where(x => x.Product_Price >= priceLow && x.Product_Price <= priceHigh).OrderBy(x => x.id).Skip(start).Take(pageSize);
        }

        //tính số page cho phần chia trang(có kèm theo price)
        public int getPageForPrice(int priceLow, int priceHigh)
        {
            int countAllProductByPrice = DbContext.Products.Where(x => x.Product_Price >= priceLow && x.Product_Price <= priceHigh).OrderBy(x => x.id).Count();
            if (countAllProductByPrice % 9 == 0)
            {
                return (countAllProductByPrice / 9);
            }
            else
            {
                return (countAllProductByPrice / 9) + 1;
            }
        }

        //lấy list product có chứa name
        public IEnumerable<Framework.Product> getProductByIdAndName(int start,int pageSize,string name)
        {
            return DbContext.Products.Where(x => x.Product_Name.Contains(name)).OrderBy(x => x.id).Skip(start).Take(pageSize);
        }

        // tính số page cho list product có chứa name
        public int getPageForName(string name)
        {
            int countAllProductByPrice = DbContext.Products.Where(x => x.Product_Name.Contains(name)).OrderBy(x => x.id).Count();
            if (countAllProductByPrice % 9 == 0)
            {
                return (countAllProductByPrice / 9);
            }
            else
            {
                return (countAllProductByPrice / 9) + 1;
            }
        }
    }
}
