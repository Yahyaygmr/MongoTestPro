using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoTestPro.Dtos.CustomerDtos;
using MongoTestPro.Dtos.OrderDtos;
using MongoTestPro.Entities;
using MongoTestPro.Services.CustomerServices;
using MongoTestPro.Services.OrderServices;

namespace MongoTestPro.Controllers
{
    public class OrderController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(ICustomerService customerService, IOrderService orderService, IMapper mapper)
        {
            _customerService = customerService;
            _orderService = orderService;
            _mapper = mapper;
        }

        public async Task<IActionResult> OrderList()
        {
            return View();
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
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> OrderTotalPrice(string id, decimal price)
        {
            return RedirectToAction("OrderList");
        }
        public async Task<List<ResultCustomerDto>> GetCustomers()
        {
            var values = await _customerService.GetAllCustomersAsync();

            return values;
        }
    }
}
