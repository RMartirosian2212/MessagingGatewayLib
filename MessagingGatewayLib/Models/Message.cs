using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingGatewayLib.Models
{
    public class Message
    {
        public string Recipient { get; init; }
        public string Content { get; init; }
    }
}
