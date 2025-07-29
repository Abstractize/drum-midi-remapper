using GUI.ViewModels;
using GUI.Views;
using Managers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using Services;
using CommunityToolkit.Maui;

namespace GUI;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

		builder.Services.AddServices();
		builder.Services.AddManagers();

		// ViewModels
		builder.Services.AddTransient<MainViewModel>();
		builder.Services.AddTransient<MainPage>();

		return builder.Build();
	}
}