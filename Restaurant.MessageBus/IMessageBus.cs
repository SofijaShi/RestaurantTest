using System;
using System.Threading.Tasks;

namespace Restaurant.MessageBus
{
    public interface IMessageBus
    {
        Task PublishMessage(BaseMessage message, string topicName);
    }
}