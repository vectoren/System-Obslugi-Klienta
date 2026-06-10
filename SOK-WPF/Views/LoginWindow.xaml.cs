using SOK_WPF.ViewModels;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace SOK_WPF.Views
{
    /// <summary>
    /// Logika interakcji dla klasy LoginPage.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            var me = (MediaElement)e.Source;
            me.Position = TimeSpan.Zero;
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            try
            {
                var datacontext = DataContext as LoginVM;
                if (datacontext._mainWindow.Visibility == Visibility.Hidden)
                    App.Current.Shutdown();
            }
            catch { }
        }
    }
}
