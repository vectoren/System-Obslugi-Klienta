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
        public MainWindow _mainWindow;
        private LoginWindow _loginWindow;

        [ObservableProperty]
        string username;
        [ObservableProperty]
        bool isLoginFailed = false;
        [ObservableProperty]
        bool isLogginingIn = false;

        public LoginVM(MainWindow mainWindow, LoginWindow loginWindow)
        {
            _mainWindow = mainWindow;
            _loginWindow = loginWindow;
        }

        [RelayCommand]
        public async Task Login(object parameter)
        {
            IsLogginingIn = true;
            var LoginAttempt = await RestService.Login(Username, (parameter as PasswordBox).Password);
            var x = await RestService.SetActive(LoginAttempt.Item3.userId);
            IsLogginingIn = false;
            if (LoginAttempt.Item2)
            {
                _mainWindow.Show();
                _loginWindow.Close();
            }
            else
            {
                IsLoginFailed = true;
            }

        }


    }
}
