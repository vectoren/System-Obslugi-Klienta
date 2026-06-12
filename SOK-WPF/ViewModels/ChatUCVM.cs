using CommunityToolkit.Mvvm.ComponentModel;
using SOK_WPF.Models;
using SOK_WPF.Services;

namespace SOK_WPF.ViewModels
{
    public partial class ChatUCVM : ObservableObject
    {

        [ObservableProperty]
        List<Dictionary<string, string>> chatHistory;

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

 
    }
}
