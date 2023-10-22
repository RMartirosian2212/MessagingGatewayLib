using MessagingGatewayLib;
using MessagingGatewayLib.Models;
using MessagingGatewayLib.Services;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

public class MessagingService : IMessagingService
{
    private readonly string _accountSid;
    private readonly string _authToken;
    private readonly string _phoneNumber;
    public MessagingService(string accountSid, string authToken, string phonenumber)
    {
        _accountSid = accountSid;
        _authToken = authToken;
        _phoneNumber = phonenumber;
        TwilioClient.Init(_accountSid, _authToken);
    }

    public async Task<bool> SendMessageAsync(Message message)
    {
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
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending message: {ex.Message}");
            return false;
        }
    }
}
