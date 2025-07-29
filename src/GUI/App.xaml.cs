using GUI.Views;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace GUI;

public partial class App : Application
{
	private readonly MainPage _mainPage;

	public App(MainPage mainPage)
	{
		InitializeComponent();

		_mainPage = mainPage;
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new NavigationPage(_mainPage));
	}
}