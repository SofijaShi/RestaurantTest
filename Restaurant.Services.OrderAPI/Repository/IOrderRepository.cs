using Microsoft.AspNetCore.Http;
using Restaurant.Services.OrderAPI.Models;

namespace Restaurant.Services.OrderAPI.Repository
{
    public interface IOrderRepository
    {
        public Task<bool> AddOrder(OrderHeader orderHeader);

        public Task UpdateOrderPaymentStatus(int orderHeaderId, bool paid);
    }
}
