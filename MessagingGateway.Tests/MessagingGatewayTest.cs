

using MessagingGatewayLib.Models;

namespace MessagingGateway.Tests
{
    public class MessagingGatewayTest
    {
        [Fact]
        public async Task TestSendMessage_Succeeds()
        {
            try
            {
                var messagingService = new MessagingService(
                    "ACCOUNT_SID",
                    "AUTH_TOKEN",
                    "PHONE_NUMBER"
                    );
                var message = new Message
                {
                    Recipient = "[RECIPIENT]",
                    Content = "[MESSAGE]"
                };

                bool result = await messagingService.SendMessageAsync(message);

                Assert.True(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in TestSendMessage_Succeeds: {ex.Message}");
                throw;
            }
        }
        [Fact]
        public async Task TestSendMail_Succeeds()
        {
            try
            {
                var mailService = new MailService(
                    "[API_KEY]",
                    "[MAIL_ADRESS]");
                var mail = new Mail()
                {
                    Recipient = "[RECIPIENT]",
                    Subject = "[MESSAGE]",
                    Content = "[MESSAGE]"
                };
                bool result = await mailService.SendMailAsync(mail);
                Assert.True(result);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in TestSendMessage_Succeeds: {ex.Message}");
                throw;
            }
        }

    }
}