using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace HomeInventory.Messages
{
    public class AlertMessage : ValueChangedMessage<string>
    {
        public AlertMessage(string value) : base(value) { }
    }
}
