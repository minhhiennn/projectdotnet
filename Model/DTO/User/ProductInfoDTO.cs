using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO.User
{
    public class ProductInfoDTO
    {
        public Framework.Product product { get; set; }
        public Framework.Company company { get; set; }
        public ProductInfoDTO(Framework.Product product,Framework.Company company)
        {
            this.product = product;
            this.company = company;
        }
    }
}
