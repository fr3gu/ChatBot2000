using System;
using System.Linq;
using System.Threading.Tasks;
using ChatBot2000.Core;
using ChatBot2000.Core.Interfaces;
using TwitchLib.Client;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Events;
using TwitchLib.Client.Extensions;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Events;

namespace ChatBot2000.Infrastructure.Twitch
{
    public class TwitchChatClient : IChatClient
    {
        private readonly TwitchClient _twitchClient;
        private readonly TaskCompletionSource<bool> _onConnectCompletionTask = new TaskCompletionSource<bool>();
        private readonly TaskCompletionSource<bool> _onDisconnectCompletionTask = new TaskCompletionSource<bool>();
        private readonly TwitchClientSettings _settings;

        public TwitchChatClient(TwitchClientSettings settings)
        {
            _settings = settings;
            var connectionCredentials = new ConnectionCredentials(settings.UserName, settings.OAuth);
            _twitchClient = new TwitchClient();
            _twitchClient.Initialize(connectionCredentials, settings.Channel);
            _twitchClient.AddChatCommandIdentifier('!');

            _twitchClient.OnJoinedChannel += TwitchClientOnOnJoinedChannel;
            _twitchClient.OnUserJoined += TwitchClientOnOnUserJoined;
            _twitchClient.OnUserLeft += TwitchClientOnOnUserLeft;
            _twitchClient.OnChatCommandReceived += TwitchClientOnOnChatCommandReceived;
        }

        private void TwitchClientOnOnConnected(object sender, OnConnectedArgs e)
        {
            _onConnectCompletionTask.SetResult(true);;
        }

        private void TwitchClientOnOnChatCommandReceived(object sender, OnChatCommandReceivedArgs e)
        {
            //switch (e.Command.CommandText)
            //{
            //    case "jello":
            //        SendMessage($"Command received: {e.Command.CommandText}");
            //        break;
            //}
            OnCommandReceived?.Invoke(this, e.ToCommandReceivedEventArgs());
        }

        private void TwitchClientOnOnUserLeft(object sender, OnUserLeftArgs e)
        {
            SendMessage($"{e.Username} is leaving. Take care, {e.Username}!");
        }

        private void TwitchClientOnOnUserJoined(object sender, OnUserJoinedArgs e)
        {
            if (e.Username != _settings.UserName)
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

            await _onConnectCompletionTask.Task;
        }

        public void SendMessage(string message)
        {
            if(_twitchClient.IsConnected) _twitchClient.SendMessage(_settings.Channel, message);
        }

        public async Task Disconnect()
        {
            if (_twitchClient.IsConnected)
            {
                _twitchClient.Disconnect();
                _twitchClient.OnDisconnected += TwitchClientOnOnDisconnected;
            }

            await _onDisconnectCompletionTask.Task;
        }

        public event EventHandler<CommandReceivedEventArgs> OnCommandReceived;

        private void TwitchClientOnOnDisconnected(object sender, OnDisconnectedEventArgs e)
        {
            _onDisconnectCompletionTask.SetResult(true);
        }
    }
}
