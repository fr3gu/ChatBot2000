using ChatBot2000.Core.Interfaces;

namespace ChatBot2000.Tests.Fakes.Messages
{
    public class AlwaysReadyMessage : IAutoMessage
    {
        public string Message => "Always Ready";

        public bool IsTimeToDispatch(int secondsPassed)
        {
            return true;
        }

        public string GetMessageInstance(int secondsPassed)
        {
            return Message;
        }
    }
}