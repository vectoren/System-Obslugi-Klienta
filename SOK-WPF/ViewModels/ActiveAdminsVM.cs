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

        public ActiveAdminsVM()
        {

        }

        public async void LoadActiveAdmins()
        {
            Accounts = await RestService.GetActiveAdmins();
        }
    }
}
