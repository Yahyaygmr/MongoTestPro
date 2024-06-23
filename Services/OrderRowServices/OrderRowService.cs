using AutoMapper;
using MongoDB.Driver;
using MongoTestPro.Dtos.OrderRowDtos;
using MongoTestPro.Entities;
using MongoTestPro.Settings;

namespace MongoTestPro.Services.OrderRowServices
{
    public class OrderRowService : IOrderRowService
    {
        private readonly IMongoCollection<OrderRow> _orderRowCollection;
        private readonly IMapper _mapper;

        public OrderRowService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            _mapper = mapper;
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _orderRowCollection = database.GetCollection<OrderRow>(databaseSettings.OrderRowCollectionName);

        }

        public async Task CreateOrderRowAsync(CreateOrderRowDto createOrderRowDto)
        {
            var result = _mapper.Map<OrderRow>(createOrderRowDto);
            await _orderRowCollection.InsertOneAsync(result);
        }

        public async Task DeleteOrderRowAsync(string id)
        {
            await _orderRowCollection.DeleteOneAsync(x => x.OrderRowId == id);
        }

        public async Task<List<ResultOrderRowDto>> GetAllOrderRowsAsync()
        {
            var values = await _orderRowCollection.Find(x => true).ToListAsync();
            var result = _mapper.Map<List<ResultOrderRowDto>>(values);
            return result;
        }

        public async Task<GetByIdOrderRowDto> GetByIdOrderRowAsync(string id)
        {
            var values = await _orderRowCollection.Find(x => x.OrderRowId == id).FirstOrDefaultAsync();
            var result = _mapper.Map<GetByIdOrderRowDto>(values);
            return result;
        }

        public async Task UpdateOrderRowAsync(UpdateOrderRowDto updateOrderRowDto)
        {
            var result = _mapper.Map<OrderRow>(updateOrderRowDto);
            await _orderRowCollection.FindOneAndReplaceAsync(x => x.OrderRowId == updateOrderRowDto.OrderRowId, result);
        }
    }
}