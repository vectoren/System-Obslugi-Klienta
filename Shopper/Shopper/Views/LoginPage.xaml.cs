namespace Shopper.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    private async void GoRegister(object sender, EventArgs e)
    {
        if (sender is Button button)
        {

            await Navigation.PushAsync(new RegisterAccountPage());
        }
    }
}