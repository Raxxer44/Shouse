using Technic.Web.Models;
using Technic.Web.Models.Categories;

namespace Technic.Web.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<Result<CategoryViewModel>> GetCategoryById(Guid CategoryId);
        Task<Result<CategoryListViewModel>> GetAllCategory();
        Task<Result<string>> CreateCategory(CreateCategoryViewModel Model);
        Task<Result<string>> DeleteCategory(Guid CategoryId);
    }
}
