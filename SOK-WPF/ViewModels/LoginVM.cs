using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SOK_WPF.Services;
using SOK_WPF.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Controls;

namespace SOK_WPF.ViewModels
{
    public partial class LoginVM : ObservableObject
    {
        private LoginWindow _loginWindow;

        [ObservableProperty]
        string username;
        [ObservableProperty]
        bool isLoginFailed = false;
        [ObservableProperty]
        bool isLogginingIn = false;

        public LoginVM(LoginWindow loginWindow)
        {
            _loginWindow = loginWindow;
        }

        [RelayCommand]
        public async Task Login(object parameter)
        {
            IsLogginingIn = true;
            var LoginAttempt = await RestService.Login(Username, (parameter as PasswordBox).Password);
            if (LoginAttempt.Item2)
            {            
                var x = await RestService.SetActive(LoginAttempt.Item3.userId);

               await ChatService.ConnectWithCookiesAsync();

                new MainWindow().Show();
                _loginWindow.Close();
            }
            else
            {
                IsLoginFailed = true;
            }
            IsLogginingIn = false;
        }


    }
}
