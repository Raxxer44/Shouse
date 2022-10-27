using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SlugGenerator;
using Technic.Web.Data.Base;
using Technic.Web.Data.Entites;
using Technic.Web.Models;
using Technic.Web.Models.Product;
using Technic.Web.Services.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace Technic.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<ProductService> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public ProductService(IMapper mapper, ILogger<ProductService> logger, IApplicationDbContext context, UserManager<User> userManager)
        {
            _mapper = mapper;
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public async Task<Result<string>> CreateProduct(CreateProductViewModel model)
        {
            var category = await _context.ProductCategories.FindAsync(model.ProductCategoryId);

            if (category == null)
            {
                _logger.LogError($"Категория с id - {model.ProductCategoryId} не найден");
                return Result<string>.Failure("Категория не найдена", 404);
            }

            if (model.Price < 0)
            {
                return Result<string>.Failure("Цена не может быть отрицательной");
            }

            if (model.Price > double.MaxValue)
            {
                return Result<string>.Failure("Цена товара не может первышать максимально допустимые значения");
            }

            string slug = model.Name.GenerateSlug();

            byte[] imageData = null;

            using (var binaryReader = new BinaryReader(model.ProductImage.OpenReadStream()))
            {
                imageData = binaryReader.ReadBytes((int)model.ProductImage.Length);
            }

            Product product = new Product();

            try
            {
                product.ProductStatus = Data.Enums.ProductStatus.inStock;
                product.ProductCategory = category;
                product.Description = model.Description;
                product.Slug = slug;
                product.Price = model.Price;
                product.Name = model.Name;
                product.ProductImage = imageData;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Маппинг завершился с ошибкой: {ex}");
                return Result<string>.Failure("Произошла внутренняя ошибка сервера", 500);
            }

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return Result<string>.Success(String.Empty);
        }

        public async Task<Result<string>> DeleteProduct(Guid ProductId)
        {
            var product = await _context.Products.FindAsync(ProductId);

            if (product == null)
            {
                _logger.LogError($"Товар с id - {ProductId} не найден");
                return Result<string>.Failure("Товар не найден", 404);
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Result<string>.Success(String.Empty);
        }

        public async Task<Result<ProductViewModel>> GetProduct(Guid ProductId)
        {
            var product = await _context.Products.FindAsync(ProductId);

            if (product == null)
            {
                _logger.LogError($"Товар с id - {ProductId} не найден");
                return Result<ProductViewModel>.Failure("Товар не найден", 404);
            }

            ProductViewModel viewModel = new ProductViewModel();

            try
            {
                viewModel = _mapper.Map<ProductViewModel>(product);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Маппинг завершился с ошибкой: {ex}");
                return Result<ProductViewModel>.Failure("Произошла внутренняя ошибка сервера", 500);
            }

            return Result<ProductViewModel>.Success(viewModel);
        }

        public async Task<Result<ShortProductListViewModel>> GetProductList()
        {
            var products = await _context.Products.ToListAsync();

            ShortProductListViewModel viewModel = new ShortProductListViewModel();

            try
            {
                if (products != null)
                {
                    foreach (var item in products)
                    {
                        viewModel.ProductList.Add(_mapper.Map<ShortProductViewModel>(item));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Маппинг завершился с ошибкой: {ex}");
                return Result<ShortProductListViewModel>.Failure("Произошла внутренняя ошибка сервера", 500);
            }

            return Result<ShortProductListViewModel>.Success(viewModel);
        }

        public async Task<Result<ShortProductListViewModel>> GetProductListByCategory(Guid CategoryId)
        {
            var category = await _context.ProductCategories.FindAsync(CategoryId);

            if (category is null)
            {
                _logger.LogError($"Категория с Id - не найдена {CategoryId}");
                return Result<ShortProductListViewModel>.Failure("Категория не найдена");
            }

            var products = await _context.Products.Where(x => x.ProductCategory == category).ToListAsync();

            ShortProductListViewModel viewModel = new ShortProductListViewModel();

            try
            {
                if (products != null)
                {
                    foreach (var item in products)
                    {
                        viewModel.ProductList.Add(_mapper.Map<ShortProductViewModel>(item));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Маппинг завершился с ошибкой: {ex}");
                return Result<ShortProductListViewModel>.Failure("Произошла внутренняя ошибка сервера", 500);
            }

            return Result<ShortProductListViewModel>.Success(viewModel);
        }
    }
}
