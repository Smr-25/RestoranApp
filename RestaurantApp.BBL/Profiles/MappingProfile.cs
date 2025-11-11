using AutoMapper;

namespace RestaurantApp.BBL.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MenuItem, MenuItemDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.TotalItemCount, opt => opt.MapFrom(src => src.OrderItems.Sum(oi => oi.Count)))
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.MenuItemName, opt => opt.MapFrom(src => src.MenuItem.Name));

            CreateMap<Category, CategoryDto>();
        }
    }
}

