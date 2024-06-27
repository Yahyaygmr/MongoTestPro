using AutoMapper;
using MongoDB.Driver;
using MongoTestPro.Dtos.OrderDtos;
using MongoTestPro.Entities;
using MongoTestPro.Services.CustomerServices;
using MongoTestPro.Settings;

namespace MongoTestPro.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly IMongoCollection<Order> _orderCollection;
        private readonly IMongoCollection<Customer> _customerCollection;
        private readonly IMapper _mapper;

        public OrderService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            _mapper = mapper;
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _orderCollection = database.GetCollection<Order>(databaseSettings.OrderCollectionName);
            _customerCollection = database.GetCollection<Customer>(databaseSettings.CustomerCollectionName);
        }

        public async Task CreateOrderAsync(CreateOrderDto createOrderDto)
        {
            var result = _mapper.Map<Order>(createOrderDto);
            await _orderCollection.InsertOneAsync(result);
        }

        public async Task DeleteOrderAsync(string id)
        {
            await _orderCollection.DeleteOneAsync(x => x.OrderId == id);
        }

        public async Task<List<ResultOrderDto>> GetAllOrdersAsync()
        {
            var values = await _orderCollection.Find(x => true).ToListAsync();
            var result = _mapper.Map<List<ResultOrderDto>>(values);
            return result;
        }

        public async Task<List<ResultAllOrdersWithCustomer>> GetAllOrdersWithCustomerAsync()
        {
            var orders = await _orderCollection.Find(x => true).ToListAsync();
            var orderList = new List<ResultAllOrdersWithCustomer>();
            foreach (var order in orders)
            {
                var customer =await _customerCollection.Find(x=> x.CustomerId == order.CustomerId).FirstOrDefaultAsync();
                var orderWithCustomer = new ResultAllOrdersWithCustomer
                {
                    OrderId = order.OrderId,
                    OrderAddress = order.OrderAddress,
                    OrderDate = order.OrderDate,
                    Status = order.Status,
                    OrderTotalPrice = order.OrderTotalPrice,
                    OrderDescription = order.OrderDescription,
                    CustomerId = order.CustomerId,
                    CustomerName = customer.Name,
                    CustomerSurname = customer.Surname,
                    CustomerAddress = customer.Address,
                    CustomerEmail = customer.Email,
                    CustomerPhoneNumber = customer.PhoneNumber,
                };
                orderList.Add(orderWithCustomer);
            }

            return orderList;
        }

        public async Task<GetByIdOrderDto> GetByIdOrderAsync(string id)
        {
            var values = await _orderCollection.Find(x => x.OrderId == id).FirstOrDefaultAsync();
            var result = _mapper.Map<GetByIdOrderDto>(values);
            return result;
        }

        public async Task UpdateOrderAsync(UpdateOrderDto updateOrderDto)
        {
            var result = _mapper.Map<Order>(updateOrderDto);
            await _orderCollection.FindOneAndReplaceAsync(x => x.OrderId == updateOrderDto.OrderId, result);
        }
    }
}