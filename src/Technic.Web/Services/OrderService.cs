using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Technic.Web.Data.Base;
using Technic.Web.Data.Entites;
using Technic.Web.Models;
using Technic.Web.Services.Interfaces;

namespace Technic.Web.Services
{
    public class OrderService : IOrderService
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderService> _logger;
        private readonly UserManager<User> _userManager;

        public OrderService(IApplicationDbContext context, IMapper mapper, ILogger<OrderService> logger, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<Result<Guid>> CreateOrder(CreateOrderViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId.ToString());

            if (user is null)
            {
                _logger.LogError($"Пользователь с Id {model.UserId} не найден");
                return Result<Guid>.Failure("Пользователь не найден");
            }

            var product = await _context.Products.FindAsync(model.ProductId);

            if (product is null)
            {
                _logger.LogError($"Пользователь с Id {model.UserId} не найден");
                return Result<Guid>.Failure("Пользователь не найден");
            }

            Order order = new Order()
            {
                Name = model.Name,
                Surname = model.Surname,
                Patronymic = model.Patronymic,
                Address = model.Address,
                PostIndex = model.Index,
                Product = product,
                User = user
            };

            product.ProductStatus = Data.Enums.ProductStatus.outStock;

            _context.Products.Update(product);
            await _context.Orders.AddAsync(order);

            await _context.SaveChangesAsync();

            return Result<Guid>.Success(Guid.Empty);
        }
    }
}
