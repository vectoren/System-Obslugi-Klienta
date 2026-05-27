using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SOK_WPF.Models;
using SOK_WPF.Views;
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
        public List<BugReport> bugReportsList = new();

        BugReports bugReportsView { get; set; }
        OrdersIssues ordersIssuesView { get; set; }

        public DashboradVM(Frame frame)
        {
            MainFrame = frame;
            bugReportsView = new BugReports(MainFrame);
            ordersIssuesView = new OrdersIssues(MainFrame);
        }


        [RelayCommand]
        void GoToOrders() => MainFrame.Content = ordersIssuesView;


        [RelayCommand]
        void GoToBugsReports() => MainFrame.Content = bugReportsView;

    }
}
