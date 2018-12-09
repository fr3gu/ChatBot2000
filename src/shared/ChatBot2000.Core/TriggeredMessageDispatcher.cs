using System;
using System.Collections.Generic;
using System.Text;

namespace ChatBot2000.Core
{
    public class TriggeredMessageDispatcher
    {
        public List<IAutoMessage> MessagesToDispatch { get; set; }
        public List<string> QueuedMessages { get; set; }

        public TriggeredMessageDispatcher()
        {
            MessagesToDispatch = new List<IAutoMessage>();
            QueuedMessages = new List<string>();
        }

        public void Publish(IAutoMessage repeatingMessage)
        {
            MessagesToDispatch.Add(repeatingMessage);
        }
    }
}
