using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SOK_WPF.Models;
using SOK_WPF.Services;
using System.Collections.ObjectModel;

namespace SOK_WPF.ViewModels
{
    public partial class ChatUCVM : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<ChatMessage> chatHistory = new();

        [ObservableProperty]
        string text;

        [ObservableProperty]
        Account? acc;

        public ChatUCVM()
        {
            if (Acc != null)
                _ = GetChat();

            ChatService.OnMessageReceived += HandleIncomingMessage;
        }

        public async Task GetChat()
        {
            ChatHistory = await RestService.GetChatHistory(RestService.account.userId, Acc.userId);
        }

        private void HandleIncomingMessage(ChatMessage message)
        {
            if (message.sender == Acc.userId)
                message.user = Acc;
            else
                message.user = RestService.account;
            ChatHistory.Add(message);

        }

        partial void OnAccChanged(Account? value)
        {
            _ = GetChat();
        }

        [RelayCommand]
        async Task SendText()
        {
            if (string.IsNullOrWhiteSpace(Text)) return;

            var message = new ChatMessage()
            {
                message = Text,
                sendDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"),
                sender = RestService.account.userId,
                recipient = Acc.userId,
                user = RestService.account
            };
            ChatService.SendMessage(message);

            Text = string.Empty;
        }
    }
}
