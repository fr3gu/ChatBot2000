using System;
using ChatBot2000.Core.Interfaces;

namespace ChatBot2000.Tests.Fakes.Messages
{
    public class NeverReadyMessage : IAutoMessage
    {
        public string Message => throw new NotImplementedException();

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
