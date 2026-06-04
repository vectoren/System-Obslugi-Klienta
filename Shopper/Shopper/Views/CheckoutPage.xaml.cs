namespace Shopper.Views;

public partial class CheckoutPage : ContentPage
{
    private bool IsUserLoggedIn = true; 
    private double BasketSubtotal = 150.00; 
    private double DeliveryCost = 14.99;

    public CheckoutPage()
    {
        InitializeComponent();
        CalculateSummary();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (!IsUserLoggedIn)
        {
            
            await DisplayAlertAsync("Wymagane logowanie", "Aby sfinalizować zamówienie, musisz być zalogowany.", "Zaloguj się");

            await Shell.Current.GoToAsync("login");
        }
    }

    private void CalculateSummary()
    {
        double total = BasketSubtotal + DeliveryCost;

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
}
