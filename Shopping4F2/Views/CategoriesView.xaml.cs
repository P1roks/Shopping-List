using Shopping4F2.ViewModels;

namespace Shopping4F2.Views;

public partial class CategoriesView : ContentPage
{
    protected override void OnAppearing()
    {
        if(BindingContext is ProductsCategoriesViewModel viewModel)
        {
            viewModel.LoadCategories();
        }
    }

    public CategoriesView()
	{
		InitializeComponent();
	}
}