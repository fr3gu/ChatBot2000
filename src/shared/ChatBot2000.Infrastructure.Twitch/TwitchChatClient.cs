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
        private readonly TaskCompletionSource<bool> _connectionCompletionTask = new TaskCompletionSource<bool>();
        private readonly TwitchClientSettings _settings;

        public TwitchChatClient(TwitchClientSettings settings)
        {
            _settings = settings;
            var connectionCredentials = new ConnectionCredentials(settings.UserName, settings.OAuth);
            _twitchClient = new TwitchClient();
            _twitchClient.Initialize(connectionCredentials, "fr3gu_");
            _twitchClient.OnJoinedChannel += TwitchClientOnOnJoinedChannel;
            _twitchClient.OnUserJoined += TwitchClientOnOnUserJoined;
            _twitchClient.OnUserLeft += TwitchClientOnOnUserLeft;
            _twitchClient.AddChatCommandIdentifier('!');
            _twitchClient.OnChatCommandReceived += TwitchClientOnOnChatCommandReceived;
        }

        private void TwitchClientOnOnConnected(object sender, OnConnectedArgs e)
        {
            _connectionCompletionTask.SetResult(true);
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
                SendMessage($"Ohai {e.Username}! \\o");
            }
        }

        private void TwitchClientOnOnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            SendMessage($"{e.BotUsername} just entered. Behave!");
        }


        public async Task Connect()
        {
            if (!_twitchClient.IsConnected)
            {
                _twitchClient.Connect();
                _twitchClient.OnConnected += TwitchClientOnOnConnected;
            }

            await _connectionCompletionTask.Task;
        }

        public void SendMessage(string message)
        {
            if(_twitchClient.IsConnected) _twitchClient.SendMessage(_settings.Channel, message);
        }
    }
}
