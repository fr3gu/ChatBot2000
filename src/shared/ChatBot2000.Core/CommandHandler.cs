using System.Collections.Generic;
using System.Linq;
using ChatBot2000.Core.Interfaces;
using ChatBot2000.Core.Messaging.Interfaces;

namespace ChatBot2000.Core
{
    public class CommandHandler
    {
        private readonly List<ICommandMessage> _commandMessages;

        public CommandHandler(IEnumerable<IChatClient> chatClients, List<ICommandMessage> commandMessages)
        {
            _commandMessages = commandMessages;
            foreach (var chatClient in chatClients)
            {
                chatClient.OnCommandReceived += CommandReceivedHandler;
            }
        }

        private void CommandReceivedHandler(object sender, CommandReceivedEventArgs e)
        {
            if (sender is IChatClient chatClient)
            {
                var commandMessage = _commandMessages.FirstOrDefault(c => c.CommandText == e.CommandWord);
                commandMessage?.Process(chatClient, e);
            }
        }
    }
}
