using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Technic.Web.Data.Enums;

namespace Technic.Web.Data.Entites
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(200)]
        public string Name { get; set; }
        [MaxLength(1500)]
        public string? Description { get; set; }
        public double Price { get; set; }
        public byte[] ProductImage { get; set; }
        [MaxLength(200)]
        public string Slug { get; set; }
        public ProductStatus ProductStatus { get; set; }
        public ProductCategory ProductCategory { get; set; }
        [ForeignKey("ProductCategory")]
        public Guid ProductCategoryId { get; set; }
        public Order Order { get; set; }
    }
}
