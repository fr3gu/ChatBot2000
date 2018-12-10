using System;
using ChatBot2000.Core.Interfaces;
using ChatBot2000.Core.Messaging;
using ChatBot2000.Core.Messaging.Interfaces;

namespace ChatBot2000.Tests.Fakes.Messages
{
    public class NeverReadyMessage : IAutoMessage
    {
        public string Message => throw new NotImplementedException();
        public DataItemStatus DataItemStatus { get; set; }

        public bool IsTimeToDispatch(long milliSecondsPassed)
        {
            return false;
        }

        public string GetMessageInstance(long milliSecondsPassed)
        {
            return Message;
        }
    }
}
