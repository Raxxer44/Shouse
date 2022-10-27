using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Technic.Web.Data.Entites;
using Technic.Web.Data.Enums;

namespace Technic.Web.Models.Product
{
    public class CreateProductViewModel
    {
        [MaxLength(200)]
        public string Name { get; set; }
        [MaxLength(1500)]
        public string? Description { get; set; }
        public double Price { get; set; }
        public IFormFile ProductImage { get; set; }
        public Guid ProductCategoryId { get; set; }
    }
}
