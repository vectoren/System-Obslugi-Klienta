using Shopper.Models;
using Shopper.Services;
using Shopper.UsageClasses;
using System.Text.Json;

namespace Shopper.Views;

public partial class CheckoutPage : ContentPage
{
    private double DeliveryCost = 14.99;
    private IDataCache _cache;
    private Account acc;
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
        else
        {
            acc = await GetCurrentUser();
            FullNameEntry.Text = acc.fullName;
            /*        StreetEntry.Text;
                    PostalCodeEntry.Text
                    CityEntry.Text
                    PhoneEntry.Text*/

            
        }
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

       

        string homeNumber = StreetEntry.Text.Split(' ').LastOrDefault() ?? "";

        //Zamowienie
        Orders order = new Orders(
                products: string.Join(",", _cache.GetAllData().Select(kvp => $"{kvp.Key.Id}x{kvp.Value}")),
                wholeCost: Convert.ToDecimal(ProductsSubtotalLabel.Text.Replace(" zł", "")) + Convert.ToDecimal(DeliveryCostLabel.Text.Replace(" zł", "")),
                orderDate: DateTime.Now.ToString("yyyy-MM-dd")
            );

        int orderId = await ProductsRestService.AddNewOrder(order);
        order.orderId = orderId;

        //Dane Platnosci

        PaymentDetails pd = new PaymentDetails(
                paymentType: selectedPayment,
                paymentDate: DateTime.Now.ToString("yyyy-MM-dd"),
                order: order
            );

        var (paymentResponse, paymentSuccess) = await ProductsRestService.AddNewPaymentDetails(pd);


        //Dane dostawy
        DeliveryDetails dd = new DeliveryDetails(
                region: GetRegion(int.Parse(PostalCodeEntry.Text.Substring(0,2))),
                city: CityEntry.Text,
                street: StreetEntry.Text,
                townCode: string.Join("-", PostalCodeEntry.Text.Substring(0,2), PostalCodeEntry.Text.Substring(2,3)),
                homeNumber: homeNumber,
                userId: acc,
                order: order
            );

        var (responseMessage, success) = await ProductsRestService.AddNewDeliveryDetails(dd);

        if(paymentSuccess && success)
        {
            _cache.ClearCache();
            await DisplayAlertAsync("SUCCESS", $"Zamówienie zostało złożone.\nWybrana płatność: {selectedPayment}! \nProsimy o cierpliwość w oczekiwaniu na paczkę.", "OK");
        }


        await Shell.Current.GoToAsync("/list");
    }

    public string GetRegion(int townCodePrefix)
    {
        return townCodePrefix switch
        {
            >= 0 and <= 9 => " mazowieckie",
            >= 10 and <= 14 => " warmińsko-mazurskie",
            19 => " warmińsko-mazurskie",
            >= 15 and <= 18 => " podlaskie",
            >= 20 and <= 24 => " lubelskie",
            >= 25 and <= 29 => " świętokrzyskie",
            >= 30 and <= 34 => " małopolskie",
            >= 35 and <= 39 => " podkarpackie (lub małopolskie)",
            >= 45 and <= 48 => " opolskie",
            >= 40 and <= 44 => " śląskie",
            49 => " śląskie",
            >= 50 and <= 59 => " dolnośląskie",
            >= 60 and <= 64 => " wielkopolskie",
            >= 65 and <= 69 => " lubuskie",
            >= 70 and <= 78 => " zachodniopomorskie",
            >= 80 and <= 84 => " pomorskie lub kujawsko-pomorskie",
            >= 85 and <= 87 => " kujawsko-pomorskie",
            >= 90 and <= 99 => " łódzkie",
            _ => "Nieznany przedział kodu"
        };
    }

    private async Task<Account> GetCurrentUser()
    {
        var userDataJson = await SecureStorage.GetAsync("currentUser");
        var acc = JsonSerializer.Deserialize<Account>(userDataJson);
        return acc;

    }
}
