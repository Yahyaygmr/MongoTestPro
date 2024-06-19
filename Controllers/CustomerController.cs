using Microsoft.AspNetCore.Mvc;
using MongoTestPro.Dtos.CustomerDtos;
using MongoTestPro.Services.CustomerServices;

namespace MongoTestPro.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        public async Task<IActionResult> CustomerList()
        {
            var values = await _customerService.GetAllCustomersAsync();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateCustomer()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDto createCustomerDto)
        {
            await _customerService.CreateCustomerAsync(createCustomerDto);

            return RedirectToAction("CustomerList");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateCustomer(string id)
        {
            var value = await _customerService.GetByIdCustomerAsync(id);
            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCustomer(UpdateCustomerDto dto)
        {
            await _customerService.UpdateCustomerAsync(dto);
            return RedirectToAction("CustomerList");
        }
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            await _customerService.DeleteCustomerAsync(id);
            return RedirectToAction("CustomerList");
        }
    }
}
