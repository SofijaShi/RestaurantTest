using Restaurant.Web.Models;

namespace Restaurant.Web.Services.IServices
{
    public interface ICartService
    {
        Task<T> GetCartByUserIdAsync<T>(string userId, string token = null);
        Task<T> AddToCartByUserIdAsync<T>(CartDto cartDto, string token = null);
        Task<T> UpdateCartByUserIdAsync<T>(CartDto cartDto, string token = null);
        Task<T> RemoveFromCartByUserIdAsync<T>(int cartId, string token = null);
    }
}
