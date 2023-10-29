using FluentValidation;
using MessagingGatewayLib.Models;
using MessagingGatewayLib.Services;
using MessagingGatewayLib.Settings;
using MessagingGatewayLib.Validations;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Mail.Model;

namespace MessagingGatewayLib
{
    public class MailService : IMailService
    {
        private readonly string _apiKey;
        private readonly SendGridClient _sendGridClient;
        private readonly string _mailAdress;
        private readonly IValidator<Mail> _mailValidation = new MailValidator();
        private readonly IValidator<MailOptions> _mailOptionsValidator = new MailOptionsValidator();
        private readonly ILogger<MailService> _logger;

        public MailService(MailOptions options, ILogger<MailService> logger)
        {
            _logger = logger;
            var validationResult = _mailOptionsValidator.Validate(options);
            if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors;
                foreach (var error in validationErrors)
                {
                    _logger.LogError($"Validation Error: {error.ErrorMessage}");
                }
                throw new Exception("Mail options validation failed");
            }
            _apiKey = options.ApiKey;
            _sendGridClient = new SendGridClient(_apiKey);
            _mailAdress = options.MailAdress;
        }

        public async Task<bool> SendMailAsync(Mail mail)
        {
            var validationResult = _mailValidation.Validate(mail);
            if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors;
                foreach (var error in validationErrors)
                {
                    _logger.LogError($"Validation Error: {error.ErrorMessage}");
                }
                return false;
            }

            try
            {
                var from = new EmailAddress(_mailAdress);
                var to = new EmailAddress(mail.Recipient);

                var msg = MailHelper.CreateSingleEmail(from, to, mail.Subject, mail.Content, mail.Content);
                var response = await _sendGridClient.SendEmailAsync(msg);

                if (response.StatusCode != System.Net.HttpStatusCode.Accepted)
                {
                    _logger.LogError("Failed to send email");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error sending email: {ex.Message}");
                return false;
            }
        }
    }
}
