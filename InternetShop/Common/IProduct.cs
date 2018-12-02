using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IProduct: IModel
    {
        int ProductId { get; set; }
        string Name { get; set; }
        decimal Cost { get; set; }
        string About { get; set; }
    }
}
