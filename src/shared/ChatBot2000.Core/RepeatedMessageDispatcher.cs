using System;
using System.Collections.Generic;
using System.Text;

namespace ChatBot2000.Core
{
    public class RepeatedMessageDispatcher
    {
        public List<RepeatedMessage> MessagesToDispatch { get; set; }
        public List<string> QueuedMessages { get; set; }

        public RepeatedMessageDispatcher()
        {
            MessagesToDispatch = new List<RepeatedMessage>();
            QueuedMessages = new List<string>();
        }

        public void Publish(RepeatedMessage repeatedMessage)
        {
            MessagesToDispatch.Add(repeatedMessage);
        }
    }
}
