using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using SOK_WPF.Models;
using SOK_WPF.Services;

namespace SOK_WPF.ViewModels
{
    [ObservableObject]
    public partial class ActiveAdminsVM
    {
        [ObservableProperty]
        List<Account>? accounts;

        [ObservableProperty]
        public ChatUCVM chatUCVM;
        
        public ActiveAdminsVM()
        {
            LoadActiveAdmins();
            ChatUCVM = new();
        }

        public async void LoadActiveAdmins()
        {
            Accounts = await RestService.GetActiveAdmins();
        }
        
    }
}
