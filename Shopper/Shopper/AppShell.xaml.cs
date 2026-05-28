using Shopper.Views;

namespace Shopper
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("account", typeof(AccountPage));
        }
    }
}
