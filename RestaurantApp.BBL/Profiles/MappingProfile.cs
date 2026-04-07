namespace RestaurantApp.BBL.Profiles;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, CategoryReturnDto>();
        
        CreateMap<MenuItem, MenuItemReturnDto>()
            .ForMember(d => d.CategoryName, opt => opt.MapFrom(s => s.Category.Name));
        CreateMap<MenuItemCreateDto, MenuItem>();
        CreateMap<MenuItemUpdateDto, MenuItem>();
        
        CreateMap<OrderItem, OrderItemReturnDto>()
            .ForMember(d => d.MenuItemName, opt => opt.MapFrom(s => s.MenuItem.Name));
        
        CreateMap<Order, OrderReturnDto>()
            .ForMember(d => d.TotalItemCount, opt => opt.MapFrom(s => s.OrderItems.Sum(o => o.Count)));
    }
}
