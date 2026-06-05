using Shopper.Models;
using Shopper.Services;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Shopper.Views;

public partial class AccountPage : ContentPage
{
	public AccountPage()
	{
		InitializeComponent();
	}
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        var json = await SecureStorage.Default.GetAsync("currentUser");
        if (json is not null)
        {
            Account currentUser = JsonSerializer.Deserialize<Account>(json);
            BindingContext = currentUser;

        }
        else
        {
            await Shell.Current.GoToAsync("/login");
        }

    }

    private void UpdateUserData(object sender, EventArgs e)
    {

    }

    private async void LogoutUser(object sender, EventArgs e)
    {
        await DBRestService.Logout();
        await Shell.Current.GoToAsync("/login");
    }

    private void DeleteUser(object sender, EventArgs e)
    {

    }
}