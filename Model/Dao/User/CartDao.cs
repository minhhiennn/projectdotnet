using Model.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao.User
{
    public class CartDao
    {
        private ProjectDotNetDbContext DbContext = null;
        public CartDao()
        {
            DbContext = new ProjectDotNetDbContext();
        }

        // lấy cart của từng user
        // nếu user chưa có cart sẽ trả về false
        public bool checkCartUser(int id)
        {
            var cart = DbContext.Carts.Find(id);
            if (cart == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // tạo cart cho user bằng id user
        public void createCart(int id)
        {
            var cart = new Framework.Cart();
            cart.id_User = id;
            DbContext.Carts.Add(cart);
            DbContext.SaveChanges();
        }

        // lấy listitem trong cart bằng id user
        public IEnumerable<Framework.Cart_item> getCartItemById(int id)
        {
            return DbContext.Cart_item.Where(x => x.id_Cart == id);
        }

        // lấy listitem DTO trong cart bằng id user
        public List<DTO.User.CartItemDTO> getCartItemDTOById(int id)
        {
            List<DTO.User.CartItemDTO> listItemDTO = new List<DTO.User.CartItemDTO>();
            var listItem = getCartItemById(id);
            foreach (var item in listItem)
            {
                var product = new ProductDao().getProductById(item.id_Product);
                int soluong = item.soluong;
                var CartItemDTO = new DTO.User.CartItemDTO(id, product, soluong);
                listItemDTO.Add(CartItemDTO);
            }
            return listItemDTO;
        }

        // tính tổng total cart bằng id user
        public double totalCart(int id)
        {
            var listItemDTO = getCartItemDTOById(id);
            double result = 0;
            foreach (var item in listItemDTO)
            {
                result += (item.product.Product_Price * item.soluong);
            }
            return result;
        }

        // thêm product vào cart của user bằng user_id
        public void addProduct(int userId, int productId, int quantity)
        {
            var cartItem = DbContext.Cart_item.SingleOrDefault(x => x.id_Cart == userId && x.id_Product == productId);
            if (cartItem == null)
            {
                var cartItem2 = new Framework.Cart_item();
                cartItem2.id_Cart = userId;
                cartItem2.id_Product = productId;
                cartItem2.soluong = quantity;
                DbContext.Cart_item.Add(cartItem2);
                DbContext.SaveChanges();
            }
            else
            {
                cartItem.soluong += quantity;
                DbContext.SaveChanges();
            }
        }

        // xóa product(cartItem) của một cart bằng user_id và product_id
        public void deleteProduct(int userId, int productId)
        {
            var cartItem = DbContext.Cart_item.SingleOrDefault(x => x.id_Cart == userId && x.id_Product == productId);
            DbContext.Cart_item.Remove(cartItem);
            DbContext.SaveChanges();
        }

        // giảm hay tăng 1 đơn vị số lượng của 1 cart item
        public void decreaseOrIncrease(int userId, int productId, string action)
        {
            var cartItem = DbContext.Cart_item.SingleOrDefault(x => x.id_Cart == userId && x.id_Product == productId);
            if (action.Equals("decrease"))
            {
                cartItem.soluong -= 1;
                DbContext.SaveChanges();
            }
            else if(action.Equals("increase"))
            {
                cartItem.soluong += 1;
                DbContext.SaveChanges();
            }
        }

        // thay đổi số lượng product của 1 cart item
        public void changeProductQuantity(int userId,int productId,int quantity)
        {
            var cartItem = DbContext.Cart_item.SingleOrDefault(x => x.id_Cart == userId && x.id_Product == productId);
            cartItem.soluong = quantity;
            DbContext.SaveChanges();
        }
    }
}
