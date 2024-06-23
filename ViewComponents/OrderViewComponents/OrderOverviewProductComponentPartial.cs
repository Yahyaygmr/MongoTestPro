using Microsoft.AspNetCore.Mvc;
using MongoTestPro.Dtos.OrderRowDtos;
using MongoTestPro.Services.OrderRowServices;
using MongoTestPro.Services.ProductServices;

namespace MongoTestPro.ViewComponents.OrderViewComponents
{
    public class OrderOverviewProductComponentPartial : ViewComponent
    {
        private readonly IOrderRowService _orderRowService;
        private readonly IProductService _productService;

        public OrderOverviewProductComponentPartial(IOrderRowService orderRowService, IProductService productService)
        {
            _orderRowService = orderRowService;
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            ViewBag.Id = id;
           return View();
        }
    }
}
