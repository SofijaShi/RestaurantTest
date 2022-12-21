using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Services.ShoppingCart.DbContexts;
using Restaurant.Services.ShoppingCart.Models;
using Restaurant.Services.ShoppingCart.Models.Dto;

namespace Restaurant.Services.ShoppingCart.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public CartRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<bool> ClearCart(string userId)
        {
            var cartHeaderFromDb = await _db.CartHeaders.FirstOrDefaultAsync(u => u.UserId == userId);
            if (cartHeaderFromDb != null)
            {
                _db.CartDetails
                    .RemoveRange(_db.CartDetails.Where(u => u.CardHeaderId == cartHeaderFromDb.CartHeaderId));
                _db.CartHeaders.Remove(cartHeaderFromDb);
                await _db.SaveChangesAsync();

                return true;
            }
            return false;
        }

        public async Task<CartDto> CreateUpdateCart(CartDto cartDto)
        {
            Cart cart = _mapper.Map<Cart>(cartDto);
            var prodInDB = _db.Products.FirstOrDefault(u => u.ProductId == cartDto.CartDetails.FirstOrDefault().ProductId);
            if (prodInDB == null)
            {
                _db.Products.Add(cart.CartDetails.FirstOrDefault().Product);
                await _db.SaveChangesAsync();
            }
            var cartHeaderFromDb = await _db.CartHeaders.AsNoTracking()
                .FirstOrDefaultAsync();
            if (cartHeaderFromDb == null)
            {
                //_db.CartHeaders.Add(cart.CartHeader);
                //await _db.SaveChangesAsync();
                //cart.CartDetails.FirstOrDefault().CardHeaderId = cart.CartHeader.CartHeaderId;
                cart.CartDetails.FirstOrDefault().Product = null;
                _db.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                await _db.SaveChangesAsync();
            }
            else
            {
                var cartDetailsFromDb = await _db.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                    u => u.ProductId == cart.CartDetails.FirstOrDefault().ProductId 
                    && u.CardHeaderId == cartHeaderFromDb.CartHeaderId);

                if (cartDetailsFromDb == null)
                {
                    cart.CartDetails.FirstOrDefault().CardHeaderId = cartHeaderFromDb.CartHeaderId;
                    cart.CartDetails.FirstOrDefault().Product = null;
                    _db.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                    await _db.SaveChangesAsync();
                }
                else
                {
                    cart.CartDetails.FirstOrDefault().Product = null;
                    cart.CartDetails.FirstOrDefault().Count+=cartDetailsFromDb.Count;
                    _db.CartDetails.Update(cart.CartDetails.FirstOrDefault());
                    await _db.SaveChangesAsync();
                }
            }
            return _mapper.Map<CartDto>(cart);
        }

        public async Task<CartDto> GetCard()
        {
            Cart cart = new()
            {
                CartHeader = await _db.CartHeaders.FirstOrDefaultAsync()
            };
            cart.CartDetails = _db.CartDetails.Include(u=>u.Product);
            return _mapper.Map<CartDto>(cart);

        }

        public async Task<bool> RemoveFromCart(int cartDetailsId)
        {
            CartDetails cartDetails = await _db.CartDetails
                .FirstOrDefaultAsync(u => u.CartDetailsId == cartDetailsId);

            int totalCountOfCartItems = _db.CartDetails
                .Where(u => u.CardHeaderId == cartDetails.CardHeaderId).Count();
            _db.CartDetails.Remove(cartDetails);
            if (totalCountOfCartItems == 1)
            {
                var cartHeadertoRemove = await _db.CartHeaders
                    .FirstOrDefaultAsync(u => u.CartHeaderId == cartDetails.CardHeaderId);
                _db.CartHeaders.Remove(cartHeadertoRemove);
            }
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
