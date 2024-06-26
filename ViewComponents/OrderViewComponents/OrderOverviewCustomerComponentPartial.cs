﻿using Microsoft.AspNetCore.Mvc;
using MongoTestPro.Services.OrderServices;

namespace MongoTestPro.ViewComponents.OrderViewComponents
{
    public class OrderOverviewCustomerComponentPartial : ViewComponent
    {
        private readonly IOrderService _orderService;

        public OrderOverviewCustomerComponentPartial(IOrderService orderService)
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
