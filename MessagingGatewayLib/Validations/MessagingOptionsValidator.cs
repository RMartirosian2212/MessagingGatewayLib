using FluentValidation;
using MessagingGatewayLib.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingGatewayLib.Validations
{
    public class MessagingOptionsValidator : AbstractValidator<MessagingOptions>
    {
        public MessagingOptionsValidator() 
        {
            RuleFor(options => options.AccountSid).NotEmpty().WithMessage("AccountSid is required");
            RuleFor(options => options.AuthToken).NotEmpty().WithMessage("AuthToken is required");
            RuleFor(options => options.PhoneNumber).NotEmpty().WithMessage("PhoneNumber is required");
        }
    }
}
