using Restaurant.Services.OrderAPI.Messages;

namespace Restaurant.Services.OrderAPI.Messaging
{
    public interface IRabbitMQConsumer
    {
        Task HandleMessage(CheckoutHeaderDto checkoutHeaderDto);
    }
}
