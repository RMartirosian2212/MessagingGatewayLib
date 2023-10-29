using FluentValidation;
using MessagingGatewayLib.Models;

namespace MessagingGatewayLib.Validations
{
    public class MailValidator : AbstractValidator<Mail>
    {
        public MailValidator()
        {
            RuleFor(model => model.Recipient).NotEmpty().Must(ContainAtSymbol);
            RuleFor(model => model.Subject).NotEmpty();
            RuleFor(model => model.Content).NotEmpty().MaximumLength(500);
        }
        private bool ContainAtSymbol(string recipient)
        {
            return !string.IsNullOrWhiteSpace(recipient) && recipient.Contains("@");
        }
    }
}
