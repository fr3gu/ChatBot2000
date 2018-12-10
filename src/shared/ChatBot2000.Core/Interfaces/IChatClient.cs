using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot2000.Core.Interfaces
{
    public interface IChatClient
    {
        Task Connect();
        void SendMessage(string message);
        Task Disconnect();

        event EventHandler<CommandReceivedEventArgs> OnCommandReceived;
    }
}
