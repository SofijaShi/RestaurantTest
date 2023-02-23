using Restaurant.MessageBus;

namespace Restaurant.Services.ShoppingCart.RabbitMqSender
{
    public interface IRabbitMQCartMessageSender
    {
        void SendMessage(BaseMessage baseMessage, String queueName);
    }
}
