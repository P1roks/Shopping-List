namespace Shopping4F2.Controls;
using Shopping4F2.Models;

public partial class RecipeProductView : ContentView
{
    public static readonly BindableProperty ProductProperty =
		BindableProperty.Create(nameof(Product), typeof(Product), typeof(RecipeProductView), new Product());

	public Models.Product Product {
		get => (Product)GetValue(ProductProperty);
        set => SetValue(ProductProperty, value);
	}

	public RecipeProductView()
	{
		InitializeComponent();
	}
}