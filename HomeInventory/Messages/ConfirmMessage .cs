using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeInventory.Messages
{
    public class ConfirmMessage : RequestMessage<bool>
    {
        public string Title { get; }
        public string Question { get; }

        public TaskCompletionSource<bool> Tcs { get; }

        public ConfirmMessage(string title, string question)
        {
            Title = title;
            Question = question;
            Tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        }
    }
}
