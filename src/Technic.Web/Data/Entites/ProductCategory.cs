using System.ComponentModel.DataAnnotations;

namespace Technic.Web.Data.Entites
{
    public class ProductCategory
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(300)]
        public string? Description { get; set; }
        [MaxLength(100)]
        public string Slug { get; set; }
        public byte[]? CategoryImage { get; set; }
        public List<Product> Products { get; set; }
    }
}
