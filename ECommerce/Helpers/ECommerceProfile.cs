using AutoMapper;
using Core.Entities;
using ECommerce.DTOs;

namespace ECommerce.Helpers
{
    public class ECommerceProfile : Profile
    {
        public ECommerceProfile()
        {
            // Customer mappings
            CreateMap<Customer, CustomerDto>().ReverseMap();

            // Product mappings
            CreateMap<Product, ProductDto>().ReverseMap();

            

            CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderProducts));

            CreateMap<OrderProduct, OrderItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Product.Price * src.Quantity));




            CreateMap<OrderCreateUpdateDto, Order>()
                .ForMember(dest => dest.OrderProducts, opt => opt.MapFrom(src => src.Items));

            CreateMap<OrderItemCreateDto, OrderProduct>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));
        }
    }
}
