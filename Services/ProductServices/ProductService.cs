using AutoMapper;
using MongoDB.Driver;
using MongoTestPro.Dtos.ProductDtos;
using MongoTestPro.Entities;
using MongoTestPro.Services.GCSServices;
using MongoTestPro.Settings;

namespace MongoTestPro.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMongoCollection<Category> _cateoryCollection;
        private readonly IMapper _mapper;
        private readonly ICloudStorageService _cloudStorageService;
        public ProductService(IMapper mapper, IDatabaseSettings databaseSettings, ICloudStorageService cloudStorageService)
        {
            _mapper = mapper;
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _productCollection = database.GetCollection<Product>(databaseSettings.ProductCollectionName);
            _cateoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _cloudStorageService = cloudStorageService;
        }
        public async Task CreateProductAsync(CreateProductDto createProductDto)
        {
            if(createProductDto.ImageFile != null)
            {
                string fileNameForStorage = FormFileName(createProductDto.Name, createProductDto.ImageFile.FileName);
                createProductDto.ImageUrl = await _cloudStorageService.UploadFileAsync(createProductDto.ImageFile, fileNameForStorage);
                createProductDto.ImageStorageName = fileNameForStorage;
            }
            var value = _mapper.Map<Product>(createProductDto);
            try
            {
                await _productCollection.InsertOneAsync(value);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
           
        }

        public async Task DeleteProductAsync(string id)
        {
            await _productCollection.DeleteOneAsync(x => x.ProductId == id);
        }

        public async Task<List<ResultProductDto>> GetAllProductsAsync()
        {
            var values = await _productCollection.Find(x => true).ToListAsync();
            var result = _mapper.Map<List<ResultProductDto>>(values);
            return result;
        }

        public async Task<List<ResultProductWithCategoryDto>> GetAllProductsWithCategoryAsync()
        {
            var products = await _productCollection.Find(x => true).ToListAsync();
            var productWithCategories = new List<ResultProductWithCategoryDto>();

            foreach (var product in products)
            {
                var category = await _cateoryCollection.Find(c => c.CategoryId == product.CategoryId).FirstOrDefaultAsync();
                var productWithCategory = new ResultProductWithCategoryDto
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Price = product.Price,
                    Stock = product.Stock,
                    ImageUrl = product.ImageUrl,
                    CategoryName = category?.CategoryName
                };
                productWithCategories.Add(productWithCategory);
            }
            return productWithCategories;
        }

        public async Task<GetByIdProductDto> GetByIdProductAsync(string id)
        {
            var values = await _productCollection.Find(x => x.ProductId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdProductDto>(values);
        }

        public async Task UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            var values = _mapper.Map<Product>(updateProductDto);

            await _productCollection.FindOneAndReplaceAsync(x => x.ProductId == updateProductDto.ProductId, values);
        }
        private static string FormFileName(string title, string fileName)
        {
            var fileExtension = Path.GetExtension(fileName);
            var fileNameForStorage = $"{title}-{DateTime.Now.ToString("yyyyMMddHHmmss")}{fileExtension}";
            return fileNameForStorage;
        }
    }
}