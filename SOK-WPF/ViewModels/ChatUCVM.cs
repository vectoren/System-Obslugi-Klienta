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
        ObservableCollection<Dictionary<string, string>> chatHistory = new();

        [ObservableProperty]
        string text;

        [ObservableProperty]
        Account? acc;

        public ChatUCVM()
        {
            _ = GetChat();

            // Subskrypcja zdarzenia z ChatService
            ChatService.OnMessageReceived += HandleIncomingMessage;
        }

        public async Task GetChat()
        {
            ChatHistory = await RestService.GetChatHistory(Acc);
        }

        private void HandleIncomingMessage(ChatMessage message)
        {
            // Gdy serwer rozgłosi nową wiadomość przez WebSocket,
            // mapujemy obiekt ChatMessage na Twój format Dictionary<string, string> i wrzucamy do UI
            var mappedMessage = new Dictionary<string, string>
            {
                { "user", message.sender.ToString() },
                { "fullName", message.sender == RestService.account?.userId ? "Ja" : "Rozmówca" },
                { "content", message.message },
                { "chatId", "0152" } // Tymczasowe sztywne ID
            };

            ChatHistory.Add(mappedMessage);
        }

        partial void OnAccChanged(Account? value)
        {
            _ = GetChat();
        }

        [RelayCommand]
        async Task SendText()
        {
            if (string.IsNullOrWhiteSpace(Text)) return;

            // Budujemy obiekt na bazie faktycznych danych wpisanych przez użytkownika
            var message = new ChatMessage()
            {
                message = Text,
                sendDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"),
                sender = 1,
                recipient = 2
            };

            // Po prostu wysyłamy przez istniejące, otwarte połączenie
            ChatService.SendMessage(message);

            // Czyszczenie pola tekstowego po wysłaniu
            Text = string.Empty;
        }
    }
}
