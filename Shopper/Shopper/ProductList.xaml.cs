using Shopper.Models;
using Shopper.Services;

namespace Shopper;

public partial class ProductList : ContentPage
{
    private readonly RestService _service = new RestService();
	public ProductList()
	{
		InitializeComponent();
	}

    private async void PobierzDane(object sender, EventArgs e)
    {
        try
        {
            List <Product> dane = await _service.GetProductsAsync();
            ProductsCollectionView.ItemsSource = dane;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Błąd połączenia", $"Nie udało się pobrać danych: {ex.Message}", "OK");
        }
    }

    private void OnProductItemTapped(object sender, TappedEventArgs e)
    {

    }

    private void OnRefreshViewRefreshing(object sender, EventArgs e)
    {

    }

    private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
    {

    }
}