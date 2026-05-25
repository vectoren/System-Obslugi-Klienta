using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SOK_WPF.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace SOK_WPF.ViewModels
{
    public partial class DashboradVM : ObservableObject
    {
        public Frame MainFrame { get; set; }

        [ObservableProperty]
        public List<BugReport> bugReports = new();
        public DashboradVM(Frame frame)
        {
            MainFrame = frame;
        }


        [RelayCommand]
        void GoToOrders() => MainFrame.Content = new Views.BugReports();


        [RelayCommand]
        void GoToBugsReports() => MainFrame.Content = new Views.BugReports();

    }
}
