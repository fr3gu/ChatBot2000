using ChatBot2000.Core.Data;
using ChatBot2000.Core.Interfaces;
using ChatBot2000.Core.Messaging.Interfaces;

namespace ChatBot2000.Core.Messaging
{
    public class StaticCommandResponseMessage : DataItem, ICommandMessage
    {
        private readonly string _staticResponse;
        public string CommandText { get; }
        public void Process(IChatClient triggeringClient, CommandReceivedEventArgs eventArgs)
        {
            triggeringClient.SendMessage(_staticResponse);
        }

        public StaticCommandResponseMessage(string commandText, string staticResponse, DataItemStatus dataItemStatus = DataItemStatus.Draft)
        {
            _staticResponse = staticResponse;
            CommandText = commandText;
            DataItemStatus = dataItemStatus;
        }
    }
}
