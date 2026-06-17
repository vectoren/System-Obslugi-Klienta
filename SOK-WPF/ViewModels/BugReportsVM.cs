using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SOK_WPF.Models;
using SOK_WPF.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;

namespace SOK_WPF.ViewModels
{
    public partial class BugReportsVM : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<BugReport>? bugReports = new();
        public Frame MainFrame { get; set; }

        [ObservableProperty]
        BugReport? selectedReport;

        public BugReportsVM(Frame frame)
        {
            MainFrame = frame;
            _ = RefreshBugReports();
        }

        public async Task RefreshBugReports()
        {
            BugReports = await RestService.GetBugReports();
        }

        
        [RelayCommand]
        public async Task GoBack() { await RefreshBugReports(); MainFrame.GoBack(); }
    }
}
