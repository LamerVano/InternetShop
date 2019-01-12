using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Order: IModel
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Count { get; set; }
        public string Status { get; set; }
    }
}
