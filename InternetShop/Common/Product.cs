using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Product: IModel
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double Cost { get; set; }
        public string About { get; set; }
    }
}
