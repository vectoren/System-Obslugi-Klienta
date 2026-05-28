using Shopper.Models;

namespace Shopper.Views;

public partial class RegisterAccountPage : ContentPage
{
	public RegisterAccountPage()
	{
		InitializeComponent();
	}

    private async void GoLogin(object sender, EventArgs e)
    {
        if (sender is Button button )
        {

            await Navigation.PushAsync(new LoginPage());
        }
    }
}