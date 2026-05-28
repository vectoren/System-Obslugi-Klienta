using Shopper.Models;
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
        // Tutaj możesz dodać logikę do zapisania nowego konta, np. wysłanie danych do serwera lub zapisanie lokalnie
        await DisplayAlertAsync("SUKCES", "Konto zostało zarejestrowane.", "OK");
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("http://10.0.2.2:8080/");
        var response = await client.PostAsJsonAsync("api/register", newAccount);
        if(response.IsSuccessStatusCode)
        {
            await DisplayAlertAsync("SUKCES", "Konto zostało zarejestrowane na serwerze.", "OK");
        }
        else
        {
            await DisplayAlertAsync("BŁĄD", "Nie udało się zarejestrować konta na serwerze.", "OK");
        }


    }
}