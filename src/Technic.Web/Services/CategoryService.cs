using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SlugGenerator;
using Technic.Web.Data.Base;
using Technic.Web.Data.Entites;
using Technic.Web.Models;
using Technic.Web.Models.Categories;
using Technic.Web.Services.Interfaces;

namespace Technic.Web.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(IMapper mapper, IApplicationDbContext context, ILogger<CategoryService> logger)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }

        public async Task<Result<string>> CreateCategory(CreateCategoryViewModel Model)
        {
            string slug = Model.Name.GenerateSlug();
            ProductCategory category = new ProductCategory();
            try
            {
                category = _mapper.Map<ProductCategory>(Model);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Маппинг завершился с ошибкой: {ex}");
                return Result<string>.Failure("Произошла внутренняя ошибка сервера", 500);
            }

            category.Slug = Model.Name.GenerateSlug();

            await _context.ProductCategories.AddAsync(category);
            await _context.SaveChangesAsync();

            return Result<string>.Success(String.Empty);
        }

        public async Task<Result<string>> DeleteCategory(Guid CategoryId)
        {
            var category = await _context.ProductCategories.FindAsync(CategoryId);

            if (category == null)
            {
                _logger.LogError($"Категория с id - {CategoryId} не найдена!");
                return Result<string>.Failure("Категория не найдена", 404);
            }

            _context.ProductCategories.Remove(category);
            await _context.SaveChangesAsync();

            return Result<string>.Success(String.Empty);
        }

        public async Task<Result<CategoryListViewModel>> GetAllCategory()
        {
            var categories = await _context.ProductCategories.ToListAsync();

            CategoryListViewModel model = new CategoryListViewModel();

            try
            {
                if (categories != null)
                {
                    foreach (var item in categories)
                    {
                        model.CategoryList.Add(_mapper.Map<CategoryViewModel>(item));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Маппинг завершился с ошибкой: {ex}");
                return Result<CategoryListViewModel>.Failure("Произошла внутренняя ошибка сервера", 500);
            }

            return Result<CategoryListViewModel>.Success(model);
        }

        public async Task<Result<CategoryViewModel>> GetCategoryById(Guid CategoryId)
        {
            var category = await _context.ProductCategories.FindAsync(CategoryId);

            if (category == null)
            {
                _logger.LogError($"Категория с id - {CategoryId} не найдена!");
                return Result<CategoryViewModel>.Failure("Категория не найдена", 404);
            }

            CategoryViewModel categoryModel = new CategoryViewModel();

            try
            {
                categoryModel = _mapper.Map<CategoryViewModel>(category);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Маппинг завершился с ошибкой: {ex}");
                return Result<CategoryViewModel>.Failure("Произошла внутренняя ошибка сервера", 500);
            }

            return Result<CategoryViewModel>.Success(categoryModel);
        }
    }
}
