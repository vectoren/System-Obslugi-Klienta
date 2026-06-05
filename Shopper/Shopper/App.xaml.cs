using Microsoft.Extensions.DependencyInjection;

namespace Shopper
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            
        }
        protected override void OnStart()
        {
            base.OnStart();
            SecureStorage.Default.RemoveAll();
        }
        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }

    }
}