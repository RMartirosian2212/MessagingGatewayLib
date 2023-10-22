using MessagingGatewayLib.Models;
using MessagingGatewayLib.Services;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace MessagingGatewayLib
{
    public class MailService : IMailService
    {
        private readonly string _apiKey;
        private readonly SendGridClient _sendGridClient;
        private readonly string _mailAdress;

        public MailService(string apiKey, string mailAdress)
        {
            _apiKey = apiKey;
            _sendGridClient = new SendGridClient(_apiKey);
            _mailAdress = mailAdress;
        }
        public async Task<bool> SendMailAsync(Mail mail)
        {
            try
            {
                var from = new EmailAddress(_mailAdress);
                var to = new EmailAddress(mail.Recipient);
                var mailOptions = new Mail()
                {
                    Subject = mail.Subject,
                    Content = mail.Content
                };
                var msg = MailHelper.CreateSingleEmail(
                    from,
                    to,
                    mailOptions.Subject,
                    mailOptions.Content,
                    mailOptions.Content
                    );
                var response = await _sendGridClient.SendEmailAsync(msg);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                return false;
            }
        }

    }
}
