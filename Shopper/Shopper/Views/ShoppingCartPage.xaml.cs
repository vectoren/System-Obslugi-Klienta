using Shopper.Models;
using Shopper.UsageClasses;
using System.Collections.ObjectModel;

namespace Shopper.Views;

public partial class ShoppingCartPage : ContentPage
{

    public ObservableCollection<CartItem> CartItems { get; set; }
    private readonly IDataCache _cache;
    public ShoppingCartPage(IDataCache cache)
	{
		InitializeComponent();
        CartItems = new ObservableCollection<CartItem>();
        _cache = cache;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var cartDetails = _cache.GetAllData();
        foreach((Product p, int q) in cartDetails)
        {
            CartItems.Add(new CartItem { Product = p, Quantity = q});
        }
        CartCollectionView.ItemsSource = CartItems;
        UpdateTotalSummary();
    }

    private void OnDecreaseQuantityClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var item = (CartItem)button.CommandParameter;

        if (item.Quantity > 1)
        {
            item.Quantity--;
        }
        else
        {
            CartItems.Remove(item);
        }
        UpdateTotalSummary();
    }

    private void OnIncreaseQuantityClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var item = (CartItem)button.CommandParameter;

        item.Quantity++;

        UpdateTotalSummary();
    }

    private void UpdateTotalSummary()
    {
        decimal total = 0;
        foreach (var item in CartItems)
        {
            total += item.TotalPrice;
        }
        CartTotalLabel.Text = $"{total:F2} zł";
    }

    private async void OnCheckoutClicked(object sender, EventArgs e)
    {
        if (CartItems.Count == 0)
        {
            await DisplayAlertAsync("ERROR", "Nie masz żadnych produktów w koszyku.", "OK");
            return;
        }
        foreach(CartItem c in CartItems)
        {
            _cache.AddData(c.Product, c.Quantity);
        }

        await Shell.Current.GoToAsync("checkout");
    }
}
