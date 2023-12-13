using Shopping4F2.ViewModels;

namespace Shopping4F2.Views;

public partial class RecipesView : ContentPage
{
    protected override void OnAppearing()
    {
        if(BindingContext is RecipesViewModel viewModel)
        {
            viewModel.LoadRecipes();
        }
    }

    public RecipesView()
	{
		InitializeComponent();
	}
}