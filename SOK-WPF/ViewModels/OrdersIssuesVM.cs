using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SOK_WPF.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Controls;

namespace SOK_WPF.ViewModels
{
    public partial class OrdersIssuesVM : ObservableObject
    {
        [ObservableProperty]
        List<ClientIssue>? issuesList = new List<ClientIssue>()
        {
        };
        public Frame MainFrame { get; set; }

        [ObservableProperty]
        ClientIssue? selectedIssue;

        public OrdersIssuesVM(Frame frame)
        {
            MainFrame = frame;
        }
        [RelayCommand]
        public void GoBack() => MainFrame.GoBack();
        [RelayCommand]
        public void ClientContact() => Process.Start($"mailto:{SelectedIssue?.ClientMail}&subject=Dotyczy zamówienia nr {SelectedIssue?.OrderId}");
    }
}
