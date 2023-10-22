using MessagingGatewayLib.Models;

namespace MessagingGatewayLib.Services
{
    public interface IMessagingService
    {
        Task<bool> SendMessageAsync(Message message);
    }
}
