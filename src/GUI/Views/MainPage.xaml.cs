using GUI.ViewModels;
using Microsoft.Maui.Controls;

namespace GUI.Views;

public partial class MainPage : ContentPage
{
	public MainPage(MainViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}