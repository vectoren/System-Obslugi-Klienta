namespace Shopper.Views;

public partial class ReportPage : ContentPage
{
	public ReportPage()
	{
		InitializeComponent();
	}

    private async void OnSubmitBugClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//list");
    }
}