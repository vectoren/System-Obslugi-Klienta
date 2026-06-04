using Shopper.Views;

namespace Shopper
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("account", typeof(AccountPage));
            Routing.RegisterRoute("login", typeof(LoginPage));
            Routing.RegisterRoute("list", typeof(ProductList));
            Routing.RegisterRoute("shopping-cart", typeof(ShoppingCartPage));
            Routing.RegisterRoute("checkout", typeof(CheckoutPage));
        }
    }
}
