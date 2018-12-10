using ChatBot2000.Core.Interfaces;
using ChatBot2000.Core.Messaging;
using ChatBot2000.Core.Messaging.Interfaces;

namespace ChatBot2000.Tests.Fakes.Messages
{
    public class AlwaysReadyMessage : IAutoMessage
    {
        public string Message => "Always Ready";
        public DataItemStatus DataItemStatus => DataItemStatus.Active;

        public bool IsTimeToDispatch(long milliSecondsPassed)
        {
            return true;
        }

        public string GetMessageInstance(long milliSecondsPassed)
        {
            return Message;
        }
    }
}
