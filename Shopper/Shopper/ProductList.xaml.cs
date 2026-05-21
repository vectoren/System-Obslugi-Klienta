using Shopper.Services;

namespace Shopper;

public partial class ProductList : ContentPage
{
    private readonly RestService _service = new RestService();
    public ProductList()
	{
		InitializeComponent();
	}

	
}