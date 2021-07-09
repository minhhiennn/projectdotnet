using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO.User
{
    public class CartItemDTO
    {
        public int id_User { get; set; }
        public Framework.Product product { get; set; }
        public int soluong { get; set; }

        public CartItemDTO(int id_User, Framework.Product product, int soluong)
        {
            this.id_User = id_User;
            this.product = product;
            this.soluong = soluong;
        }
    }
}
