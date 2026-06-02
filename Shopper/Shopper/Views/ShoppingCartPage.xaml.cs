using Shopper.Models;
using System.Collections.ObjectModel;

namespace Shopper.Views;

public partial class ShoppingCartPage : ContentPage
{

    public ObservableCollection<CartItem> CartItems { get; set; }
    public ShoppingCartPage()
	{
		InitializeComponent();

        CartItems = new ObservableCollection<CartItem>();
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
        if (CartItems.Count == 0) return;

        await DisplayAlert("Kasa", "Przekierowanie do płatności...", "OK");
    }
}
