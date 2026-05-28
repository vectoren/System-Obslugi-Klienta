using Shopper.Models;
using Shopper.Services;

namespace Shopper;

public partial class ProductList : ContentPage
{
    private readonly RestService _service = new RestService();
	public ProductList()
	{
		InitializeComponent();
        PobierzDane();
	}

    private async void PobierzDane()
    {
        try
        {
            List <Product> dane = await _service.GetProductsAsync();
            ProductsCollectionView.ItemsSource = dane;
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Błąd połączenia", $"Nie udało się pobrać danych: {ex.Message}", "OK");
        }
    }

    private async void OnProductItemTapped(object sender, TappedEventArgs e)
    {
        if (sender is Border border && border.BindingContext is Product wybranyProdukt)
        {
       
            await Navigation.PushAsync(new ProductDetails(wybranyProdukt));
        }
    }

    private void OnRefreshViewRefreshing(object sender, EventArgs e)
    {

    }

    private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
    {

    }
}