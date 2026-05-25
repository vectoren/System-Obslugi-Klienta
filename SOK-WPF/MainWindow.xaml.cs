using SOK_WPF.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SOK_WPF
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Dashboard dashboard = new Dashboard();


        public MainWindow()
        {
            InitializeComponent();
            dashboard.DataContext = new ViewModels.DashboradVM(MainFrame);


            MainFrame.Content = dashboard;

        }
    }
}
