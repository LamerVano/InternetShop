using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class Category: IModel
    {
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
