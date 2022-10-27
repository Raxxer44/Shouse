using Technic.Web.Models;

namespace Technic.Web.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Result<Guid>> CreateOrder(CreateOrderViewModel model);
    }
}
