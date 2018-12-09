using ChatBot2000.Core.Interfaces;

namespace ChatBot2000.Core.Messages
{
    public class RepeatingMessage : IAutoMessage
    {
        private long _lastRun;
        /// <summary>
        /// Gets the delay in milliseconds, i.e. repeat every x milliseconds
        /// </summary>
        public long RepeatEvery { get; }
        public string Message { get; }

        public RepeatingMessage(long repeatEvery, string message)
        {
            _lastRun = 0;
            RepeatEvery = repeatEvery;
            Message = message;
        }

        public bool IsTimeToDispatch(long milliSecondsPassed)
        {
            if (RepeatEvery == 0) return false;

            // ReSharper disable once InvertIf
            if (milliSecondsPassed >= _lastRun + RepeatEvery)
            {
                return true;
            }
            return false;
        }

        public string GetMessageInstance(long milliSecondsPassed)
        {
            _lastRun = milliSecondsPassed;
            return Message;
        }
    }
}
