using System;
using System.Threading.Tasks;
using ChatBot2000.Core.Interfaces;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace ChatBot2000.Infrastructure.Twitch
{
    public class TwitchChatClient : IChatClient
    {
        private readonly TwitchClient _twitchClient;

        public TwitchChatClient(string userName, string oAuth)
        {
            var connectionCredentials = new ConnectionCredentials("chatbot2k", "oauth:vf3y61q70ara81seel2rmpne3ycdqb");
            _twitchClient = new TwitchClient();
            _twitchClient.Initialize(connectionCredentials, "fr3gu_");
            _twitchClient.Connect();
            _twitchClient.OnJoinedChannel += TwitchClientOnOnJoinedChannel;
            _twitchClient.OnUserJoined += TwitchClientOnOnUserJoined;
            _twitchClient.OnUserLeft += TwitchClientOnOnUserLeft;
            _twitchClient.AddChatCommandIdentifier('!');
            _twitchClient.OnChatCommandReceived += TwitchClientOnOnChatCommandReceived;
        }

        private void TwitchClientOnOnChatCommandReceived(object sender, OnChatCommandReceivedArgs e)
        {
            SendMessage($"Command received: {e.Command.CommandText}");
        }

        private void TwitchClientOnOnUserLeft(object sender, OnUserLeftArgs e)
        {
            SendMessage($"{e.Username} is leaving. Take care, {e.Username}!");
        }

        private void TwitchClientOnOnUserJoined(object sender, OnUserJoinedArgs e)
        {
            if (e.Username != "chatbot2k")
            {
                SendMessage($"Ohai {e.Username}");
            }
        }

        private void TwitchClientOnOnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            SendMessage($"{e.BotUsername} just entered. Behave!");
        }


        public void Connect()
        {
            if (!_twitchClient.IsConnected)
            {
                _twitchClient.Connect();
            }
        }

        public void SendMessage(string message)
        {
            if(_twitchClient.IsConnected) _twitchClient.SendMessage("fr3gu_", message);
        }
    }
}
