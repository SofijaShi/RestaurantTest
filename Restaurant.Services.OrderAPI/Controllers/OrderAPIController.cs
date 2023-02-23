using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Services.OrderAPI.Repository;

namespace Restaurant.Services.OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderAPIController : ControllerBase
    {
        private IOrderRepository _orderRepository;

        public OrderAPIController(IOrderRepository orderRepository)
        {
           _orderRepository = orderRepository;
        }
    }
}
