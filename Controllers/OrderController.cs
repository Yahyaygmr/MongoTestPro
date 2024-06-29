using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoTestPro.Dtos.CustomerDtos;
using MongoTestPro.Dtos.OrderDtos;
using MongoTestPro.Dtos.OrderRowDtos;
using MongoTestPro.Entities;
using MongoTestPro.Services.CustomerServices;
using MongoTestPro.Services.OrderRowServices;
using MongoTestPro.Services.OrderServices;
using MongoTestPro.Services.ProductServices;

namespace MongoTestPro.Controllers
{
    public class OrderController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;
        private readonly IOrderRowService _orderRowService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public OrderController(ICustomerService customerService, IOrderService orderService, IMapper mapper, IOrderRowService orderRowService, IProductService productService)
        {
            _customerService = customerService;
            _orderService = orderService;
            _mapper = mapper;
            _orderRowService = orderRowService;
            _productService = productService;
        }

        public async Task<IActionResult> OrderList()
        {
            var orders = await _orderService.GetAllOrdersWithCustomerAsync();
            return View(orders);
        }
        public async Task<IActionResult> OrderDetail(string id)
        {
            //var order = await _orderService.GetByIdOrderAsync(id);
            //ViewBag.OrderCustomerId = order.CustomerId;
            ViewBag.orderId = id;
            var rows = await _orderRowService.GetAllOrderRowsAsync();
            var rowList = rows.Where(x => x.OrderId == id).ToList();
            List<ResultOrderRowWithProduct> resultOrderRowWithOrder = new List<ResultOrderRowWithProduct>();
            decimal totalPrice = 0;
            foreach (var row in rowList)
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
                    ProductPrice = product.Price,
                };
                resultOrderRowWithOrder.Add(resultOrder);
            }
            ViewBag.totalPrice = totalPrice;
            return View(resultOrderRowWithOrder);
        }
        [HttpGet]
        public async Task<IActionResult> CreateOrder()
        {
            ViewBag.customers = await GetCustomers();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(Order order)
        {
            order.OrderDate = DateTime.Now;
            var result = _mapper.Map<CreateOrderDto>(order);
            await _orderService.CreateOrderAsync(result);
            var gen = await _orderService.GetAllOrdersAsync();
            var gen1 = gen.FindLast(x => true);
            return RedirectToAction("ProductListForOrderRow","OrderRow", new { id = gen1.OrderId });
        }
        public async Task<IActionResult> OrderOverview(string id)
        {
            ViewBag.orderId = id;
            var rows = await _orderRowService.GetAllOrderRowsAsync();
            var rowList = rows.Where(x => x.OrderId == id).ToList();
            List<ResultOrderRowWithProduct> resultOrderRowWithOrder = new List<ResultOrderRowWithProduct>();
            decimal totalPrice = 0;
            foreach (var row in rowList)
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
                    ProductPrice = product.Price,
                };
                resultOrderRowWithOrder.Add(resultOrder);
            }
            ViewBag.totalPrice = totalPrice;
            return View(resultOrderRowWithOrder);
        }
        [HttpPost]
        public async Task<IActionResult> OrderTotalPrice(string id, decimal price)
        {
            var order = await _orderService.GetByIdOrderAsync(id);
            order.OrderTotalPrice = price;
            var result1 = _mapper.Map<Order>(order);
            var result2 = _mapper.Map<UpdateOrderDto>(result1);
            await _orderService.UpdateOrderAsync(result2);
            return RedirectToAction("OrderList");
        }
        public async Task<IActionResult> DeleteOrder(string id)
        {
            var rows = await _orderRowService.GetAllOrderRowsAsync();
            if (rows.Any(x=>x.OrderId == id))
            {
                var rowList = rows.Where(x=>x.OrderId == id).ToList();
                foreach (var row in rowList)
                {
                    await _orderRowService.DeleteOrderRowAsync(row.OrderRowId);
                }
            }
            await _orderService.DeleteOrderAsync(id);
            return RedirectToAction("OrderList", "Order");
        }
        public async Task<List<ResultCustomerDto>> GetCustomers()
        {
            var values = await _customerService.GetAllCustomersAsync();

            return values;
        }
       
    }
}
