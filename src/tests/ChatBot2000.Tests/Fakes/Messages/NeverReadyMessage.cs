using System;
using ChatBot2000.Core.Interfaces;

namespace ChatBot2000.Tests.Fakes.Messages
{
    public class NeverReadyMessage : IAutoMessage
    {
        public string Message => throw new NotImplementedException();

        public bool IsTimeToDispatch(int secondsPassed)
        {
            return false;
        }

        public string GetMessageInstance(int secondsPassed)
        {
            return Message;
        }
    }
}
