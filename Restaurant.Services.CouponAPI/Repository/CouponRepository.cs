using AutoMapper;
using Restaurant.Services.CouponAPI.DbContexts;
using Restaurant.Services.CouponAPI.Models;
using Restaurant.Services.CouponAPI.Models.Dto;

namespace Restaurant.Services.CouponAPI.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public CouponRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<CouponDto> GetCouponByCode(string couponCode)
        {
            Coupon coupon = _db.Coupons.Where(x => x.CouponCode == couponCode).FirstOrDefault();
            return _mapper.Map<CouponDto>(coupon);
        }
    }
}
