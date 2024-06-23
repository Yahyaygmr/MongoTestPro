using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoTestPro.Dtos.OrderRowDtos;
using MongoTestPro.Dtos.ProductDtos;
using MongoTestPro.Entities;
using MongoTestPro.Services.OrderRowServices;
using MongoTestPro.Services.ProductServices;

namespace MongoTestPro.Controllers
{
    public class OrderRowController : Controller
    {
        private readonly IProductService _productService;
        private readonly IOrderRowService _orderRowService;
        private readonly IMapper _mapper;

        public OrderRowController(IProductService productService, IOrderRowService orderRowService, IMapper mapper)
        {
            _productService = productService;
            _orderRowService = orderRowService;
            _mapper = mapper;
        }

        public async Task<IActionResult> ProductListForOrderRow(string id)
        {
            ViewBag.orderId = id;
            var values = await _productService.GetAllProductsWithCategoryAsync();
            return View(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRow(string productId, string orderId, int productCount)
        {
            if(productCount<1)
            {
                return RedirectToAction("ProductListForOrderRow", "OrderRow", new { id = orderId });
            }
            var rowlist = await _orderRowService.GetAllOrderRowsAsync();
            var row = rowlist.FirstOrDefault(x => x.OrderId == orderId && x.ProductId == productId);
            var productDto = await _productService.GetByIdProductAsync(productId);
            var product = _mapper.Map<Product>(productDto);
            if (row != null)
            {
                row.ProductCount += productCount;
                row.RowPrice += product.Price * productCount;
                var resultRow = _mapper.Map<UpdateOrderRowDto>(row);
                await _orderRowService.UpdateOrderRowAsync(resultRow);
            }
            else
            {
                CreateOrderRowDto orderRow = new CreateOrderRowDto()
                {
                    ProductId = productId,
                    OrderId = orderId,
                    ProductCount = productCount,
                    RowPrice = product.Price * productCount
                };
                await _orderRowService.CreateOrderRowAsync(orderRow);
            }
            product.Stock -= productCount;
            var result = _mapper.Map<Product>(product);
            var endResult = _mapper.Map<UpdateProductDto>(result);
            await _productService.UpdateProductAsync(endResult);

            return RedirectToAction("ProductListForOrderRow", "OrderRow", new { id = orderId });
        }
    }
}
