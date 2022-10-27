using Microsoft.AspNetCore.Mvc;
using Technic.Web.Services;
using Technic.Web.Services.Interfaces;

namespace Technic.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly ILogger<CategoryController> _logger;
        public CategoryController(ICategoryService categoryService, IProductService productService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _productService = productService;
            _logger = logger;
        }

        [HttpGet("{Controller}")]
        public async Task<IActionResult> All()
        {
            var result = await _categoryService.GetAllCategory();
            
            if (result.Succeeded)
            {
                return View(result.Value);
            }

            return RedirectToAction("Error", "Site", new
            {
                code = result.StatusCode
            });
        }

        [HttpGet("{Controller}/{Slug}")]
        public async Task<IActionResult> Show()
        {
            try
            {
                var result = await _productService.GetProductList();

                if (!result.Succeeded)
                {
                    return result.StatusCode == null ? View() : StatusCode(result.StatusCode);
                }

                return View(result.Value);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Задача завершилась с ошибкой {ex}");
            }

            return View();
        }
    }
}
