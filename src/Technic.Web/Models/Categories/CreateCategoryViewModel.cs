using System.ComponentModel.DataAnnotations;

namespace Technic.Web.Models.Categories
{
    public class CreateCategoryViewModel
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string? Description { get; set; }
        public byte[]? CategoryImage { get; set; }
    }
}
