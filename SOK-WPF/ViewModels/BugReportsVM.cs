using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SOK_WPF.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace SOK_WPF.ViewModels
{
    public partial class BugReportsVM : ObservableObject
    {
        [ObservableProperty]
        List<BugReport>? bugReports = new List<BugReport>()
        {
            new(){Name = "Brak możliwości zakupu", Description = "Po naciśnięciu przycisku \"Zapłać\" nic się nie dzieje.", ReportDay = new(2026, 05, 12)},
            new(){Name = "Niepełny koszyk", Description = "Dodawanie artykułu do koszyka nie zawsze działa.", ReportDay = new(2026, 05, 12)}
        };
        public Frame MainFrame { get; set; }

        [ObservableProperty]
        BugReport? selectedReport;

        public BugReportsVM(Frame frame)
        {
            MainFrame = frame;
        }
        [RelayCommand]
        public void GoBack() => MainFrame.GoBack();
    }
}
