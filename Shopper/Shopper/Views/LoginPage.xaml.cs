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
            await Task.Yield();

            var res = await DBRestService.Login(_email, _password);
            if (string.IsNullOrEmpty(res.Item2))
            {
                var ddict = new Dictionary<string, object>()
                {
                    { "account", res.Item1! }
                };
                await Shell.Current.GoToAsync("list", ddict);
            }
            else
            {
                throw new Exception(res.Item2);
            }
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