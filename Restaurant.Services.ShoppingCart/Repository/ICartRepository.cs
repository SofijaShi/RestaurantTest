using Restaurant.Services.ShoppingCart.Models.Dto;

namespace Restaurant.Services.ShoppingCart.Repository
{
    public interface ICartRepository
    {
        Task<CartDto> GetCard();
        Task<CartDto> CreateUpdateCart(CartDto cartDto);
        Task<bool> RemoveFromCart(int cartDetailsId);
        Task<bool> ApplyCoupon(string couponCode);
        Task<bool> RemoveCoupon();
        Task<bool> ClearCart(string userId);
    }
}
