using AutoMapper;
using RestaurantApp.BBL.DTOs.Categories;
using RestaurantApp.BBL.DTOs.MenuItems;
using RestaurantApp.BBL.DTOs.OrderItems;
using RestaurantApp.BBL.DTOs.Orders;

namespace RestaurantApp.BBL.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MenuItem, MenuItemReturnDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<Order, OrderReturnDto>()
                .ForMember(dest => dest.TotalItemCount, opt => opt.MapFrom(src => src.OrderItems.Sum(oi => oi.Count)))
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

            CreateMap<OrderItem, OrderItemReturnDto>()
                .ForMember(dest => dest.MenuItemName, opt => opt.MapFrom(src => src.MenuItem.Name));

            CreateMap<Category, CategoryReturnDto>();
        }
    }
}

