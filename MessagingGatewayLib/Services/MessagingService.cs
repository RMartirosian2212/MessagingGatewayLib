using FluentValidation;
using MessagingGatewayLib;
using MessagingGatewayLib.Models;
using MessagingGatewayLib.Services;
using MessagingGatewayLib.Settings;
using MessagingGatewayLib.Validations;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

public class MessagingService : IMessagingService
{
    private readonly string _accountSid;
    private readonly string _authToken;
    private readonly string _phoneNumber;
    private readonly IValidator<Message> _messageValidation = new MessageValidator();
    private readonly IValidator<MessagingOptions> _messagingOptionsValidation = new MessagingOptionsValidator();
    private readonly ILogger<MessagingService> _logger;

    public MessagingService(MessagingOptions options, ILogger<MessagingService> logger)
    {
        _logger = logger;
        var validationResult = _messagingOptionsValidation.Validate(options);
        if (!validationResult.IsValid)
        {
            var validationErrors = validationResult.Errors;
            foreach (var error in validationErrors)
            {
                _logger.LogError($"Validation Error: {error.ErrorMessage}"); 
            }
            throw new Exception("Message options validation failed");
        }
        _accountSid = options.AccountSid;
        _authToken = options.AuthToken;
        _phoneNumber = options.PhoneNumber;
        TwilioClient.Init(_accountSid, _authToken);
    }

    public async Task<bool> SendMessageAsync(Message message)
    {
        var validationResult = _messageValidation.Validate(message);
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
            var to = new PhoneNumber(message.Recipient);
            var from = new PhoneNumber(_phoneNumber);

            var messageOptions = new CreateMessageOptions(to)
            {
                Body = message.Content,
                From = from,
            };

            var twilioMessage = await MessageResource.CreateAsync(messageOptions);
            if (twilioMessage.Status == MessageResource.StatusEnum.Queued)
            {
                return true;
            }
            _logger.LogError("Failed to send message");
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error sending message: {ex.Message}");
            return false;
        }
    }
}
