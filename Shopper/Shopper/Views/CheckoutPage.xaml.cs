using Shopper.Models;
using Shopper.UsageClasses;
using System.Text.Json;

namespace Shopper.Views;

public partial class CheckoutPage : ContentPage
{
    private double DeliveryCost = 14.99;
    private IDataCache _cache;
    public CheckoutPage(IDataCache cache)
    {
        InitializeComponent();
        _cache = cache;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (await SecureStorage.GetAsync("currentUser") is null)
        {
            
            await DisplayAlertAsync("Wymagane logowanie", "Aby sfinalizować zamówienie, musisz być zalogowany.", "Zaloguj się");

            await Shell.Current.GoToAsync("login");
        }
        Account acc = await GetCurrentUser();
        FullNameEntry.Text = acc.fullName;
        StreetEntry.Text
        PostalCodeEntry.Text
        CityEntry.Text
        PhoneEntry.Text
        CalculateSummary();

    }

    private void CalculateSummary()
    {
        Dictionary<Product, int> cartItems = _cache.GetAllData();
        double BasketSubtotal = 0;
        foreach ((Product p, int q) in cartItems)
        {
            BasketSubtotal += Convert.ToDouble(p.Price * q);
        }
        double total = DeliveryCost + BasketSubtotal;

        ProductsSubtotalLabel.Text = $"{BasketSubtotal:F2} zł";
        DeliveryCostLabel.Text = $"{DeliveryCost:F2} zł";
        TotalOrderLabel.Text = $"{total:F2} zł";
    }

    private async void OnOrderSubmittedClicked(object sender, EventArgs e)
    {
        
        if (string.IsNullOrWhiteSpace(FullNameEntry.Text) ||
            string.IsNullOrWhiteSpace(StreetEntry.Text) ||
            string.IsNullOrWhiteSpace(PostalCodeEntry.Text) ||
            string.IsNullOrWhiteSpace(CityEntry.Text) ||
            string.IsNullOrWhiteSpace(PhoneEntry.Text))
        {
            await DisplayAlertAsync("Błąd", "Proszę uzupełnić wszystkie dane dostawy.", "OK");
            return;
        }

        
        string selectedPayment = "Nie wybrano";
        if (BlikRadio.IsChecked) selectedPayment = "BLIK";
        else if (CardRadio.IsChecked) selectedPayment = "Karta płatnicza";
        else if (TransferRadio.IsChecked) selectedPayment = "Przelew online";
        else if (CodRadio.IsChecked) selectedPayment = "Za pobraniem";

        
        await DisplayAlertAsync("Sukces!", $"Zamówienie zostało złożone.\nWybrana płatność: {selectedPayment}", "OK");

        
        await Shell.Current.GoToAsync("//list");
    }

    private async Task<Account> GetCurrentUser()
    {
        var userDataJson = await SecureStorage.GetAsync("currentUser");
        var acc = JsonSerializer.Deserialize<Account>(userDataJson);
        return acc;

    }
}
