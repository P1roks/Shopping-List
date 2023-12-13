using System.Diagnostics;

namespace Shopping4F2.Views;

public partial class AddProductView : ContentPage
{
	public AddProductView()
	{
		InitializeComponent();
	}

    private async void AddButton_Clicked(object sender, EventArgs e)
    {
		string name = NameEntry.Text ?? "Unknown";
		string unit = UnitEntry.Text ?? "pcs";
		string category = CategoryPicker.SelectedItem as string ?? "N/A";
		string shop = ShopEntry.Text;
		uint count = uint.TryParse(CountEntry.Text, out var temp) ? temp : 1;
		bool optional = OptionalCheckBox.IsChecked;

		Models.Product product = new(name,unit,category,shop,count,optional);

		var queryParams = new Dictionary<string, object>()
		{
			{"added_product", product},
		};
		await Shell.Current.GoToAsync("..", queryParams);
    }
}