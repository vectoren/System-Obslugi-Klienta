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

        protected BugReports bugReportsView { get; set; }
        protected OrdersIssues ordersIssuesView { get; set; }
        [ObservableProperty]
        public BugReportsVM bugRepVM;
        [ObservableProperty]
        public OrdersIssuesVM orderIssVM;

        public DashboradVM(Frame frame)
        {
            MainFrame = frame;
            bugReportsView = new BugReports(MainFrame);
            ordersIssuesView = new OrdersIssues(MainFrame);
            BugRepVM = bugReportsView.DataContext as BugReportsVM;
            OrderIssVM = ordersIssuesView.DataContext as OrdersIssuesVM;
        }


        [RelayCommand]
        void GoToOrders() => MainFrame.Navigate(ordersIssuesView);


        [RelayCommand]
        void GoToBugsReports() => MainFrame.Navigate(bugReportsView);

    }
}
