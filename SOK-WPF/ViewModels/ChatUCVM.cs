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
        ObservableCollection<Dictionary<string, string>> chatHistory;

        [ObservableProperty]
        string text;

        [ObservableProperty]
        Account? acc;


        public ChatUCVM()
        {
            _ = GetChat();
        }

        public async Task GetChat()
        {
            ChatHistory = await RestService.GetChatHistory(Acc);
        
        }


        partial void OnAccChanged(Account? value)
        {
            using var task = GetChat();
        }

        [RelayCommand]
        async Task SendText()
        {
            Dictionary<string, string> messageInfo = new()
                {
                    {"user", $"{RestService.account.userId ?? 0}"},
                    {"fullName", $"{RestService.account.fullName ?? "Current User"}"},
                    {"content", $"{Text}"},
                    {"chatId", $"{chatHistory[0]["chatId"]}"}
                };
            //bool sendRequest = await RestService.SendText(messageInfo);
#if DEBUG
            ChatHistory.Add(messageInfo);
#else
            if (sendRequest)
                await GetChat();
#endif
                }

 
    }
}
