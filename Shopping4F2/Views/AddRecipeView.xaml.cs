using Shopping4F2.Controls;

namespace Shopping4F2.Views;

public partial class AddRecipeView : ContentPage
{
	public AddRecipeView()
	{
		InitializeComponent();
	}

    private void AddProduct_Clicked(object sender, EventArgs e)
    {
		ProductsFlex.Add(new AddRecipeProductView());
    }

    private async void AddRecipe_Clicked(object sender, EventArgs e)
    {
        string name = NameEntry.Text ?? "Unknown";
        string description = DescriptionEditor.Text ?? string.Empty;
        List<Models.Product> products = new(from item in ProductsFlex.Skip(1) select ((AddRecipeProductView)item).GetProduct());
        Models.Recipe recipe = new(name, description, products);
        Storage.AddRecipe(recipe);
        await Shell.Current.GoToAsync("..");
    }
}