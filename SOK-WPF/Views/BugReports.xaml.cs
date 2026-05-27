using SOK_WPF.ViewModels;
using System.Windows.Controls;

namespace SOK_WPF.Views
{
    /// <summary>
    /// Logika interakcji dla klasy BugReports.xaml
    /// </summary>
    public partial class BugReports : Page
    {
        public BugReports(Frame frame)
        {
            DataContext = new BugReportsVM(frame);
            InitializeComponent();
        }
    }
}
