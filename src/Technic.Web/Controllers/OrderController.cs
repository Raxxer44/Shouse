using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Technic.Web.Data.Entites;
using Technic.Web.Models;
using Technic.Web.Services.Interfaces;

namespace Technic.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ILogger _logger;
        private readonly UserManager<User> _userManager;

        private OrderPageViewModel orderPageViewModel = new OrderPageViewModel();

        public OrderController(IOrderService orederService, ILogger logger = null, UserManager<User> userManager = null)
        {
            _orderService = orederService;
            _logger = logger;
            _userManager = userManager;
        }

        Guid ProductId = Guid.Empty;

        [HttpGet]
        public ActionResult Create(Guid productId)
        {
            orderPageViewModel.Id = productId;
            return View(orderPageViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Auth", new { area = "Account" });
                }

                Guid UserId = new Guid();

                try
                {
                    UserId = Guid.Parse(_userManager.GetUserId(User));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Задача завершилисаь с ошибкой: {ex}");
                }

                try
                {
                    model.UserId = UserId;

                    var result = await _orderService.CreateOrder(model);

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
            }

            return View();
        }
    }
}
