using CommunityToolkit.Mvvm.ComponentModel;
using SOK_WPF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOK_WPF.ViewModels
{
    [ObservableObject]
    public partial class DashboradVM
    {
        [ObservableProperty]
        public List<BugReport> bugReports = new();
        public DashboradVM()
        {
        }
    }
}
