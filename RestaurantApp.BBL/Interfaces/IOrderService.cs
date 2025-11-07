﻿using RestaurantApp.Core.Models;

namespace RestaurantApp.BBL.Interfaces
{
    public interface IOrderService
    {
        Task AddOrderAsync(Order order);
        Task RemoveOrderAsync(int id);

        Task<Order?> GetOrderByDateAsync(DateTime date);

        Task<Order?> GetOrderByIdAsync(int id);

        Task<List<Order>> GetOrdersByPriceIntervalAsync(decimal minPrice, decimal maxPrice);
    }
}
