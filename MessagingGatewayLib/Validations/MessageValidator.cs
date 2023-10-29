using FluentValidation;
using MessagingGatewayLib.Models;

namespace MessagingGatewayLib.Validations
{
    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator() 
        {
            RuleFor(model => model.Recipient).NotEmpty()
           .Matches("^[0-9+]+$");

           RuleFor(model => model.Content).NotEmpty();
        }
    }
}
