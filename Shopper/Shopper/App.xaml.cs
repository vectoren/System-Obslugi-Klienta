using Microsoft.Extensions.DependencyInjection;

namespace Shopper
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new ProductList());
        }

        
    }
}