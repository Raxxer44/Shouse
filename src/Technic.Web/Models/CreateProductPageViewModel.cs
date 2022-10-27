using Technic.Web.Models.Categories;
using Technic.Web.Models.Product;

namespace Technic.Web.Models
{
    public class CreateProductPageViewModel
    {
        public CategoryListViewModel Model1 { get; set; } = new CategoryListViewModel();
        public CreateProductViewModel Model2 { get; set; }
    }
}
