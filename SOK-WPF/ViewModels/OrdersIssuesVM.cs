using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SOK_WPF.Models;
using SOK_WPF.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Controls;

namespace SOK_WPF.ViewModels
{
    public partial class OrdersIssuesVM : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<ClientIssue>? issuesList = new();
        public Frame MainFrame { get; set; }

        [ObservableProperty]
        ClientIssue? selectedIssue;

        public OrdersIssuesVM(Frame frame)
        {
            MainFrame = frame;
            _ = RefreshIssuesList();
        }

        async Task RefreshIssuesList()
        {
            IssuesList = await RestService.GetClientIssues();
        }

        [RelayCommand]
        public async Task GoBack(){ MainFrame.GoBack(); await RefreshIssuesList(); }
        [RelayCommand]
        public void ClientContact() => Process.Start(new ProcessStartInfo() { FileName = $"mailto:abc?subject=Dotyczy zamówienia nr woow", UseShellExecute = true });
        //public void ClientContact() => Process.Start(new ProcessStartInfo() { FileName = $"mailto:{SelectedIssue?.ClientMail}?subject=Dotyczy zamówienia nr {SelectedIssue?.OrderId}", UseShellExecute = true });
    }
}
