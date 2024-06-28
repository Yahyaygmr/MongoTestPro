using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoTestPro.Dtos.ProductDtos;
using MongoTestPro.Entities;
using MongoTestPro.Services.CategoryServices;
using MongoTestPro.Services.ProductServices;

namespace MongoTestPro.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, ICategoryService categoryService, IMapper mapper)
        {
            _productService = productService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<IActionResult> ProductList()
        {
            var values = await _productService.GetAllProductsWithCategoryAsync();

            return View(values);
        }
        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            ViewBag.categories = await GetCategoriesAsync();
            return View();
        }
        [HttpPost]
        public IActionResult CreateProduct(CreateProductDto createProductDto)
        {
            _productService.CreateProductAsync(createProductDto);
            // 3 saniye bekle
            Thread.Sleep(4000);

            return RedirectToAction("ProductList");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateProduct(string id)
        {
            var value = await _productService.GetByIdProductAsync(id);
            ViewBag.categories = await GetCategoriesAsync();
            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto dto)
        {
            await _productService.UpdateProductAsync(dto);
            return RedirectToAction("ProductList");
        }
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productService.DeleteProductAsync(id);
            return RedirectToAction("ProductList");
        }
        public async Task<List<Category>> GetCategoriesAsync()
        {
            var values = await _categoryService.GetAllCategoriesAsync();
            var result = _mapper.Map<List<Category>>(values);
            return result;
        }
    }
}