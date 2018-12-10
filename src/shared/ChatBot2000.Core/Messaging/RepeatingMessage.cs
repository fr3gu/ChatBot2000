using ChatBot2000.Core.Data;
using ChatBot2000.Core.Messaging.Interfaces;

namespace ChatBot2000.Core.Messaging
{
    public class RepeatingMessage : DataItem, IAutoMessage
    {
        private long _lastRun;

        public string GetMessageInstance(long milliSecondsPassed)
        {
            _lastRun = milliSecondsPassed;
            return Message;
        }

        public bool IsTimeToDispatch(long milliSecondsPassed)
        {
            if (RepeatEvery == 0) return false;

            return milliSecondsPassed >= _lastRun + RepeatEvery;
        }

        public string Message { get; }

        /// <summary>
        /// Gets the delay in milliseconds, i.e. repeat every x milliseconds
        /// </summary>
        public long RepeatEvery { get; }

        public RepeatingMessage()
        {

        }

        public RepeatingMessage(long repeatEvery, string message, DataItemStatus dataItemStatus = DataItemStatus.Draft)
        {
            _lastRun = 0;
            RepeatEvery = repeatEvery;
            Message = message;
            DataItemStatus = dataItemStatus;
        }
    }
}
