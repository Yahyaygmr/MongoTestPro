using MongoTestPro.Dtos.OrderDtos;

namespace MongoTestPro.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        public Task CreateOrderAsync(CreateOrderDto createOrderDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOrderAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ResultOrderDto>> GetAllOrdersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GetByIdOrderDto> GetByIdOrderAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOrderAsync(UpdateOrderDto updateOrderDto)
        {
            throw new NotImplementedException();
        }
    }
}