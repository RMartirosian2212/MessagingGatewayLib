using MessagingGatewayLib.Models;

namespace MessagingGatewayLib.Services
{
    public interface IMailService
    {
        Task<bool> SendMailAsync(Mail mail);
    }
}
