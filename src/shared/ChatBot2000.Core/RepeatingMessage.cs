using System;
using System.Collections.Generic;
using System.Text;
using ChatBot2000.Core.Helpers;

namespace ChatBot2000.Core
{
    public interface IAutoMessage
    {
        string Message { get; }
        bool IsTimeToDispatch(int secondsPassed);
    }

    public class RepeatingMessage : IAutoMessage
    {
        private DateTime _lastRun;
        /// <summary>
        /// Gets the delay in seconds, i.e. repeat every x seconds
        /// </summary>
        public int RepeatEvery { get; }
        public string Message { get; }

        public RepeatingMessage(int repeatEvery, string message)
        {
            _lastRun = new DateTime(1800, 1, 1, 0, 0, 0);
            RepeatEvery = repeatEvery;
            Message = message;
        }

        public bool IsTimeToDispatch(int secondsPassed)
        {
            if (RepeatEvery == 0) return false;

            // ReSharper disable once InvertIf
            if (secondsPassed % RepeatEvery == 0)
            {
                _lastRun = SystemTimeService.Now();
                return true;
            }
            return false;
        }
    }
}
