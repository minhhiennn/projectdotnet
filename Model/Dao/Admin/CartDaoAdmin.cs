using Model.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao.Admin
{
    public class CartDaoAdmin
    {
        private ProjectDotNetDbContext DbContext = null;
        public CartDaoAdmin()
        {
            DbContext = new ProjectDotNetDbContext();
        }
        public void deleteCartItem(int userId)
        {
            var cartItem = DbContext.Cart_item.SingleOrDefault(x=>x.id_Cart == userId);
            if (cartItem != null)
            {
                DbContext.Cart_item.Remove(cartItem);
                DbContext.SaveChanges();
            }
        }
        public void deleteCart(int userId)
        {
            var cart = DbContext.Carts.Find(userId);
            if (cart != null)
            {
                DbContext.Carts.Remove(cart);
                DbContext.SaveChanges();
            }
        }
    }
}
