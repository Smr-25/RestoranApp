namespace RestaurantApp.BBL.Dtos
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MenuItem, MenuItemReturnDto>();
            CreateMap<Order, OrderReturnDto>()
                .ForMember(dest => dest.TotalItemCount, opt => opt.MapFrom(src => src.OrderItems.Sum(oi => oi.Count)))
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));
            CreateMap<OrderItem, OrderItemReturnDto>();
            CreateMap<Category, CategoryReturnDto>();
        }
    }
}

