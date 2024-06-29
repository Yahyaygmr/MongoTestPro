using Microsoft.AspNetCore.Mvc;
using MongoTestPro.Services.OrderServices;

namespace MongoTestPro.ViewComponents.OrderDetailViewComponents
{
    public class OrderDetailResultOrderAndCustomerComponentPartial : ViewComponent
    {
        private readonly IOrderService _orderService;

        public OrderDetailResultOrderAndCustomerComponentPartial(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var orderWithCustomer = await _orderService.GetOrderByIdWithCustomerAsync(id);
            return View(orderWithCustomer);
        }
    }
}
