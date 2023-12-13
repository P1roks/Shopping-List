namespace Shopping4F2.Controls;
using Shopping4F2.ViewModels;

public partial class RecipeView : ContentView
{
    public static readonly BindableProperty RecipeProperty =
		BindableProperty.Create(nameof(Recipe), typeof(RecipeViewModel), typeof(RecipeView), new RecipeViewModel());

	public RecipeViewModel Recipe {
		get => (RecipeViewModel)GetValue(RecipeProperty);
        set => SetValue(RecipeProperty, value);
	}

	public RecipeView()
	{
		InitializeComponent();
	}
}