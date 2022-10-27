using Microsoft.AspNetCore.Mvc;
using Technic.Web.Models;
using Technic.Web.Models.Product;
using Technic.Web.Services.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace Technic.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ILogger<ProductController> _logger;

        CreateProductPageViewModel CreateProductPageViewModel = new CreateProductPageViewModel();

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var result = await _categoryService.GetAllCategory();

                    if (!result.Succeeded)
                    {
                        return result.StatusCode == null ? View() : StatusCode(result.StatusCode);
                    }

                    CreateProductPageViewModel.Model1 = result.Value;
                    CreateProductPageViewModel.Model2 = new CreateProductViewModel();

                    return View(CreateProductPageViewModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Задача завершилисаь с ошибкой: {ex}");
                }
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductViewModel model)
        {
            try
            {
                var result = await _productService.CreateProduct(model);

                if (!result.Succeeded)
                {
                    return result.StatusCode == null ? View() : StatusCode(result.StatusCode);
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Задача завершилисаь с ошибкой: {ex}");
            }

            return View();
        }
    }
}
