using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Restaurant.Web.Models;
using Restaurant.Web.Services.IServices;

namespace Restaurant.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public CartController(IProductService productService, ICartService cartService)
        {
            _productService = productService;
            _cartService = cartService;
        }

        private async Task<CartDto> LoadCartDto()
        {
            var response = await _cartService.GetCartAsync<ResponseDto>();
            CartDto cartDto = new();

            if(response != null && response.IsSuccess)
            {
                cartDto = JsonConvert.DeserializeObject<CartDto>(Convert.ToString(response.Result));
            }
            if (cartDto.CartHeader == null)
            {
                cartDto.CartHeader = new();
                foreach(var detail in cartDto.CartDetails)
                {
                    cartDto.CartHeader.OrderTotal+= (detail.Product.Price * detail.Count);
                }
            }
            return cartDto;
        }
        public async Task<IActionResult> CartIndex()
        {
            return View(await LoadCartDto());
        }

        public async Task<IActionResult> Remove(int cartDetailsId)
        {
            var response = await _cartService.RemoveFromCartAsync<ResponseDto>(cartDetailsId, "");
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }
    }
}
