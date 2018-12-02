using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Basket: IBasket
    {
        public int BasketId { get; set; }
        public List<int> Products { get; set; }//List of ProductsId
    }
}
