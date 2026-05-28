using Shopper.Models;

namespace Shopper;

public partial class ProductDetails : ContentPage
{
	public Product SelectedProduct { get; set; }
	public ProductDetails(Product product)
	{
		InitializeComponent();
        SelectedProduct = product;
        if (SelectedProduct != null)
        {
            
            Title = SelectedProduct.Title;
            ProductImage.Source = SelectedProduct.Image;
            TitleLabel.Text = SelectedProduct.Title;
            CategoryLabel.Text = SelectedProduct.Category.ToUpper();
            PriceLabel.Text = $"{SelectedProduct.Price:F2} PLN";
            DescriptionLabel.Text = SelectedProduct.Description;
        }
    }
}