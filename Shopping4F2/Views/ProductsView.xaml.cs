using Shopping4F2.ViewModels;
using System.Diagnostics;

namespace Shopping4F2.Views;

public partial class ProductsView : ContentPage
{
    protected override void OnAppearing()
    {
        if(BindingContext is ProductsViewModel viewModel)
        {
            viewModel.LoadProducts();
        }
    }

    public ProductsView()
	{
		InitializeComponent();
	}
}