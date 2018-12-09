namespace ChatBot2000.Core.Interfaces
{
    public interface IAutoMessage
    {
        string Message { get; }
        bool IsTimeToDispatch(long milliSecondsPassed);
        string GetMessageInstance(long milliSecondsPassed);
    }
}
