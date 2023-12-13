namespace Shopping4F2.Controls;
using Shopping4F2.ViewModels;
using System.Diagnostics;

public partial class ProductView : ContentView
{
    public static readonly BindableProperty VmProperty =
		BindableProperty.Create(nameof(Vm), typeof(ProductViewModel), typeof(ProductView), new ProductViewModel());

	public ProductViewModel Vm {
		get => (ProductViewModel)GetValue(VmProperty);
        set => SetValue(VmProperty, value);
	}
    public ProductView()
	{
		InitializeComponent();
		Trace.WriteLine(Vm.Name);
	}
}