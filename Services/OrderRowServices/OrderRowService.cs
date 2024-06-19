using MongoTestPro.Dtos.OrderRowDtos;

namespace MongoTestPro.Services.OrderRowServices
{
    public class OrderRowService : IOrderRowService
    {
        public Task CreateOrderRowAsync(CreateOrderRowDto createOrderRowDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOrderRowAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ResultOrderRowDto>> GetAllOrderRowsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GetByIdOrderRowDto> GetByIdOrderRowAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOrderRowAsync(UpdateOrderRowDto updateOrderRowDto)
        {
            throw new NotImplementedException();
        }
    }
}