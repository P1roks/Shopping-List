namespace Shopping4F2.Controls;

public partial class AddRecipeProductView : ContentView
{
	public AddRecipeProductView()
	{
		InitializeComponent();
	}

	public Models.Product GetProduct()
	{
		string name = NameEntry.Text ?? "Unknown";
		string unit = UnitEntry.Text ?? "pcs";
		string category = CategoryPicker.SelectedItem as string ?? "N/A";
		uint count = uint.TryParse(CountEntry.Text, out var temp) ? temp : 1;

		return new Models.Product(name, unit, category, count);
	}
}