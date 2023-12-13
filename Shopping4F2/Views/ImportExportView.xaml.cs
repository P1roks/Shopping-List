using Microsoft.VisualBasic.FileIO;
using System.Diagnostics;

namespace Shopping4F2.Views;

public partial class ImportExportView : ContentPage
{
	public ImportExportView()
	{
		InitializeComponent();
	}

    private async void Import_Clicked(object sender, EventArgs e)
    {
        try
        {
            var file = await FilePicker.Default.PickAsync();
            if(file != null && file.FileName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
            {
                Storage.Import(file.FullPath);
            }
        }
        catch
        {
            return;
        }
    }

    private async void Export_Clicked(object sender, EventArgs e)
    {
        string location = Path.Combine(SpecialDirectories.Desktop, "shopping.xml");
        Storage.SaveTo(Path.Combine(location));
        await Shell.Current.DisplayAlert("Saved!", $"Saved to Location: {location}", "Ok");
    }
}