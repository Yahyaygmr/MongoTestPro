using MongoTestPro.Dtos.OrderRowDtos;

namespace MongoTestPro.Services.OrderRowServices
{
    public interface IOrderRowService
    {
         Task<List<ResultOrderRowDto>> GetAllOrderRowsAsync();
        Task CreateOrderRowAsync(CreateOrderRowDto createOrderRowDto);
        Task UpdateOrderRowAsync(UpdateOrderRowDto updateOrderRowDto);
        Task DeleteOrderRowAsync(string id);
        Task<GetByIdOrderRowDto> GetByIdOrderRowAsync(string id);
    }
}