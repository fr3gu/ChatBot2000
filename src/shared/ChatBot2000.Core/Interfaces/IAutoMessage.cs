namespace ChatBot2000.Core.Interfaces
{
    public interface IAutoMessage
    {
        string Message { get; }
        bool IsTimeToDispatch(int secondsPassed);
        string GetMessageInstance(int secondsPassed);
    }
}