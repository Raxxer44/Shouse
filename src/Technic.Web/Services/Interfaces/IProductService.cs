using Technic.Web.Models;
using Technic.Web.Models.Product;

namespace Technic.Web.Services.Interfaces
{
    public interface IProductService
    {
        Task<Result<string>> CreateProduct(CreateProductViewModel model);
        Task<Result<string>> DeleteProduct(Guid ProductId);
        Task<Result<ProductViewModel>> GetProduct(Guid ProductId);
        Task<Result<ShortProductListViewModel>> GetProductList();
        Task<Result<ShortProductListViewModel>> GetProductListByCategory(Guid CategoryId);
    }
}
