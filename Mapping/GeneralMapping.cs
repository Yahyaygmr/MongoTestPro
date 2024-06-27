using AutoMapper;
using MongoTestPro.Dtos.CategoryDtos;
using MongoTestPro.Dtos.CustomerDtos;
using MongoTestPro.Dtos.OrderDtos;
using MongoTestPro.Dtos.OrderRowDtos;
using MongoTestPro.Dtos.ProductDtos;
using MongoTestPro.Entities;

namespace MongoTestPro.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Category, ResultCategoryDto>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
            CreateMap<Category, GetByIdCategoryDto>().ReverseMap();

            CreateMap<Product, ResultProductDto>().ReverseMap();
            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();
            CreateMap<Product, GetByIdProductDto>().ReverseMap();

            CreateMap<Customer, ResultCustomerDto>().ReverseMap();
            CreateMap<Customer, CreateCustomerDto>().ReverseMap();
            CreateMap<Customer, UpdateCustomerDto>().ReverseMap();
            CreateMap<Customer, GetByIdCustomerDto>().ReverseMap();

            CreateMap<Order, ResultOrderDto>().ReverseMap();
            CreateMap<Order, CreateOrderDto>().ReverseMap();
            CreateMap<Order, UpdateOrderDto>().ReverseMap();
            CreateMap<Order, GetByIdOrderDto>().ReverseMap();
            CreateMap<Order, ResultAllOrdersWithCustomer>().ReverseMap();

            CreateMap<OrderRow, ResultOrderRowDto>().ReverseMap();
            CreateMap<OrderRow, CreateOrderRowDto>().ReverseMap();
            CreateMap<OrderRow, UpdateOrderRowDto>().ReverseMap();
            CreateMap<OrderRow, GetByIdOrderRowDto>().ReverseMap();
            CreateMap<OrderRow, ResultOrderRowDto>().ReverseMap();
        }
    }
}