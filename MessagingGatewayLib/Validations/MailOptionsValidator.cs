using FluentValidation;
using MessagingGatewayLib.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingGatewayLib.Validations
{
    public class MailOptionsValidator : AbstractValidator<MailOptions>
    {
        public MailOptionsValidator() 
        {
            RuleFor(model => model.ApiKey).NotEmpty().WithMessage("ApiKey is required");
            RuleFor(model => model.MailAdress).NotEmpty().WithMessage("Mail is required").Must(ContainAtSymbol);
        }
        private bool ContainAtSymbol(string recipient)
        {
            return !string.IsNullOrWhiteSpace(recipient) && recipient.Contains("@");
        }
    }
}
