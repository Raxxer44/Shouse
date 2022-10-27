using Technic.Web.Data.Entites;
using Technic.Web.Data.Enums;

namespace Technic.Web.Models.Product
{
    public class ShortProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public byte[] ProductImage { get; set; }
        public ProductStatus ProductStatus { get; set; }
    }
}
