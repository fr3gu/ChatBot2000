using ChatBot2000.Core.Data.Interfaces;

namespace ChatBot2000.Core.Messaging.Interfaces
{
    public interface IAutoMessage
    {
        string Message { get; }
        bool IsTimeToDispatch(long milliSecondsPassed);
        string GetMessageInstance(long milliSecondsPassed);
    }
}
