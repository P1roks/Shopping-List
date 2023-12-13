namespace Shopping4F2.Controls;
using Shopping4F2.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

public partial class CategoryView : ContentView
{
    public static readonly BindableProperty GroupProperty =
		BindableProperty.Create(nameof(Group), typeof(CategoryViewModel), typeof(CategoryView), new CategoryViewModel());

	public CategoryViewModel Group {
		get => (CategoryViewModel)GetValue(GroupProperty);
        set => SetValue(GroupProperty, value);
	}

	public CategoryView()
	{
		InitializeComponent();
	}
}