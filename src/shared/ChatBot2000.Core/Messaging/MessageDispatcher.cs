using System.Collections.Generic;
using System.Linq;
using ChatBot2000.Core.Interfaces;
using ChatBot2000.Core.Messaging.Interfaces;

namespace ChatBot2000.Core.Messaging
{
    public class MessageDispatcher
    {
        public List<IAutoMessage> MessagesToDispatch { get; }
        public List<string> QueuedMessages { get; }

        public MessageDispatcher()
        {
            MessagesToDispatch = new List<IAutoMessage>();
            QueuedMessages = new List<string>();
        }

        public void Publish(IAutoMessage repeatingMessage)
        {
            MessagesToDispatch.Add(repeatingMessage);
        }

        public bool CheckMessages(int secondsPassed)
        {
            if (!MessagesToDispatch.Any()) return false;

            foreach (var message in MessagesToDispatch.Where(m => m.IsTimeToDispatch(secondsPassed)))
            {
                QueuedMessages.Add(message.GetMessageInstance(secondsPassed));
            }

            return QueuedMessages.Any();

        }

        public bool TryDequeueMessage(out string message)
        {
            message = default(string);
            if(!QueuedMessages.Any()) return false;
            message = QueuedMessages.First();
            QueuedMessages.Remove(message);

            return true;
        }
    }
}
