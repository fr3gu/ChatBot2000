using ChatBot2000.Core.Interfaces;

namespace ChatBot2000.Core.Messages
{
    public class RepeatingMessage : IAutoMessage
    {
        private int _lastRun;
        /// <summary>
        /// Gets the delay in seconds, i.e. repeat every x seconds
        /// </summary>
        public int RepeatEvery { get; }
        public string Message { get; }

        public RepeatingMessage(int repeatEvery, string message)
        {
            _lastRun = 0;
            RepeatEvery = repeatEvery;
            Message = message;
        }

        public bool IsTimeToDispatch(int secondsPassed)
        {
            if (RepeatEvery == 0) return false;

            // ReSharper disable once InvertIf
            if (secondsPassed >= _lastRun + RepeatEvery)
            {
                return true;
            }
            return false;
        }

        public string GetMessageInstance(int secondsPassed)
        {
            _lastRun = secondsPassed;
            return Message;
        }
    }
}
