using Shopper.Services;

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

    private async void Login(object sender, EventArgs e)
    {
        string _email = email.Text;
        string _password = password.Text;

        try
        {
            LoadingOverlay.IsVisible = true;
            await Task.Run(async () =>
            {
                bool res = await DBRestService.Login(_email, _password);
                if (res)
                {
                    await Dispatcher.DispatchAsync(() =>
                    {
                        Shell.Current.GoToAsync("list");
                    });

                }
                else
                {
                    await Dispatcher.DispatchAsync(async () =>
                    {
                       throw new Exception("Login failed. Please check your credentials and try again.");
                    });
                }

            });
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("ERROR", ex.Message, "OK");
        }
        finally
        {
            LoadingOverlay.IsVisible = false;
        }
    }
}