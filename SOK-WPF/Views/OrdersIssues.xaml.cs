using SOK_WPF.ViewModels;
using System.Windows.Controls;

namespace SOK_WPF.Views
{
    /// <summary>
    /// Logika interakcji dla klasy OrdersIssues.xaml
    /// </summary>
    public partial class OrdersIssues : Page
    {
        public OrdersIssues(Frame frame)
        {
            DataContext = new OrdersIssuesVM(frame);
            InitializeComponent();
        }
    }
}
