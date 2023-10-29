

using MessagingGatewayLib.Models;
using MessagingGatewayLib.Services;
using MessagingGatewayLib.Settings;
using Microsoft.Extensions.Logging;
using Moq;

namespace MessagingGateway.Tests
{
    public class MessagingGatewayTest
    {
        [Fact]
        public async Task TestSendMessage_Succeeds()
        {
            var logger = new Mock<ILogger<MessagingService>>();

            MessagingOptions options = new MessagingOptions()
            {
                AccountSid = "[ACCOUNT_SID]",
                AuthToken = "[AUTH_TOKEN]",
                PhoneNumber = "[PHONE_NUMBER]"
            };
            MessagingService messagingService = new MessagingService(options, logger.Object);

            var message = new Message
            {
                Recipient = "[RECIPIENT]",
                Content = "[MESSAGE]"
            };

            var result = await messagingService.SendMessageAsync(message);
            Assert.True(result);
        }
        [Fact]
        public async Task TestSendMail_Succeeds()
        {
            var logger = new Mock<ILogger<MailService>>();
            MailOptions mailOptions = new MailOptions() 
            { 
                ApiKey = "API_KEY",
                MailAdress = "[MAIL_ADRESS]"
            };
            var mailService = new MailService(mailOptions, logger.Object);
            var mail = new Mail()
            {
                Recipient = "[RECIPIENT]",
                Subject = "[MESSAGE]",
                Content = "[MESSAGE]"
            };
            bool result = await mailService.SendMailAsync(mail);
            Assert.True(result);
        }

    }
}