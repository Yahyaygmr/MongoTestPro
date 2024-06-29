using MongoTestPro.Dtos.OrderDtos;

namespace MongoTestPro.Services.OrderServices
{
    public interface IOrderService
    {
        Task<List<ResultOrderDto>> GetAllOrdersAsync();
        Task<List<ResultAllOrdersWithCustomer>> GetAllOrdersWithCustomerAsync();
        Task<ResultOrderByIdWithCustomer> GetOrderByIdWithCustomerAsync(string id);
        Task CreateOrderAsync(CreateOrderDto createOrderDto);
        Task UpdateOrderAsync(UpdateOrderDto updateOrderDto);
        Task DeleteOrderAsync(string id);
        Task<GetByIdOrderDto> GetByIdOrderAsync(string id);
    }
}