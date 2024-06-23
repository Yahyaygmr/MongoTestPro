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
            var rows = await _orderRowService.GetAllOrderRowsAsync();
            var rowList = rows.Where(x=> x.OrderId == id).ToList();
            List<ResultOrderRowWithProduct> resultOrderRowWithOrder = new List<ResultOrderRowWithProduct>();
            decimal totalPrice = 0;
            foreach ( var row in rowList )
            {
                totalPrice += row.RowPrice;
                var product = await _productService.GetByIdProductAsync(row.ProductId);
                var resultOrder = new ResultOrderRowWithProduct
                {
                    OrderId = row.OrderId,
                    OrderRowId = row.OrderRowId,
                    ProductCount = row.ProductCount,
                    ProductId = row.ProductId,
                    RowPrice = row.RowPrice,
                    ProductImgUrl = product.ImageUrl,
                    ProductName = product.Name,
                };
                resultOrderRowWithOrder.Add( resultOrder );
            }
            ViewBag.totalPrice = totalPrice;
            return View(resultOrderRowWithOrder);
        }
    }
}
