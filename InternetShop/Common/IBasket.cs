using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IBasket: IModel
    {
        int BasketId { get; set; }
        List<int> Products { get; set; }
    }
}
