using ChatBot2000.Core.Data;
using ChatBot2000.Core.Data.Interfaces;
using ChatBot2000.Core.Interfaces;

namespace ChatBot2000.Core.Messaging.Interfaces
{
    public interface ICommandMessage : IDataItem
    {
        string CommandText { get; }
        void Process(IChatClient triggeringClient, CommandReceivedEventArgs eventArgs);

    }
}
