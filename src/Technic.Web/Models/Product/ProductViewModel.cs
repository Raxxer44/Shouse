using Technic.Web.Data.Enums;

namespace Technic.Web.Models.Product
{
    public class ProductViewModel
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public double PriceOld { get; set; }
        public byte[] ProductImage { get; set; }
        public string Slug { get; set; }
        public ProductStatus ProductStatus { get; set; }
    }
}
