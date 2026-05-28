using Shopper.Models;
using Shopper.Services;
using System.Net;
using System.Net.Http.Json;

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

    private async void Register(object sender, EventArgs e)
    {
        if(String.IsNullOrEmpty(fname.Text) || String.IsNullOrEmpty(lname.Text) || String.IsNullOrEmpty(email.Text) || String.IsNullOrEmpty(password.Text))
        {
            await DisplayAlertAsync("ERROR", "Proszę wypełnić wszystkie pola.", "OK");
            return;
        }
        Account newAccount = new Account(fname.Text, lname.Text, email.Text, password.Text);
        try
        {
            RegisterLoadingGif.IsVisible = true;
            await Task.Run(async () =>
            {
                (string, bool) res = await DBRestService.RegisterUser(newAccount);
                if (res.Item2)
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
            RegisterLoadingGif.IsVisible = false;
        }
       

    }
}