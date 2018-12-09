using System;
using System.Collections.Generic;
using System.Text;

namespace ChatBot2000.Core
{
    public class TriggeredMessageDispatcher
    {
        public List<RepeatingMessage> MessagesToDispatch { get; set; }
        public List<string> QueuedMessages { get; set; }

        public TriggeredMessageDispatcher()
        {
            MessagesToDispatch = new List<RepeatingMessage>();
            QueuedMessages = new List<string>();
        }

        public void Publish(RepeatingMessage repeatingMessage)
        {
            MessagesToDispatch.Add(repeatingMessage);
        }
    }
}
