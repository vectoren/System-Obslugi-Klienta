using System.Text.RegularExpressions;

namespace Shopper.Views;

public partial class ForgotPassPage : ContentPage
{
	public ForgotPassPage()
	{
		InitializeComponent();
	}

    private async void OnSendResetLinkClicked(object sender, EventArgs e)
    {
        string email = EmailEntry.Text?.Trim();

       
        if (string.IsNullOrWhiteSpace(email))
        {
            await DisplayAlert("Błąd", "Wprowadź swój adres e-mail.", "OK");
            return;
        }

        var emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        if (!Regex.IsMatch(email, emailRegex))
        {
            await DisplayAlert("Błąd", "Wprowadzony adres e-mail jest niepoprawny.", "OK");
            return;
        }

        
        SuccessMessageLabel.Text = $"Wysłaliśmy instrukcje resetowania hasła na adres: {email}. Sprawdź swoją skrzynkę pocztową (oraz folder Spam).";
        EmailFormState.IsVisible = false;
        BackToLoginLink.IsVisible = false;
        SuccessState.IsVisible = true;
    }

    private async void OnBackToLoginClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("login");
    }
}