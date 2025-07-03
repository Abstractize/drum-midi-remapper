//using Managers.Contracts;
using Models;
using GUI.Extensions;
using System.Diagnostics;
using Managers.Contracts;

namespace GUI;

public partial class MainPage : ContentPage
{
	private readonly IMidiMapManager _midiMapManager;
	private readonly Dictionary<string, DrumMapType> _drumMapTypes;
	private string? _selectedFilePath = null;

	public MainPage(IMidiMapManager midiMapManager)
	{
		InitializeComponent();

		_midiMapManager = midiMapManager;

		_drumMapTypes = Enum.GetValues<DrumMapType>().Cast<DrumMapType>()
				.ToDictionary(type => type.ToString().PascalCaseToUpperWithSpaces(), type => type);

		SourceMapPicker.ItemsSource = _drumMapTypes.Keys.ToList();
		TargetMapPicker.ItemsSource = _drumMapTypes.Keys.ToList();

		SourceMapPicker.SelectedIndex = 1;
		TargetMapPicker.SelectedIndex = 0;
	}

	private async void OnBrowseFileClicked(object sender, EventArgs e)
	{
		var midiFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
		{
			{ DevicePlatform.WinUI, new[] { ".mid", ".midi" } },
			{ DevicePlatform.MacCatalyst, new[] { ".mid", ".midi" } }
		});

		var result = await FilePicker.Default.PickAsync(new PickOptions
		{
			PickerTitle = "Select a MIDI file",
			FileTypes = midiFileType
		});

		if (result != null)
		{
			_selectedFilePath = result.FullPath;
			SelectedFileLabel.Text = Path.GetFileName(_selectedFilePath);
		}
	}

	private async void OnRemapClicked(object sender, EventArgs e)
	{
		if (string.IsNullOrWhiteSpace(_selectedFilePath) ||
			SourceMapPicker.SelectedItem == null ||
			TargetMapPicker.SelectedItem == null)
		{
			await DisplayAlert("Missing Data", "Please select a MIDI file and both maps.", "OK");
			return;
		}

		try
		{
			string sourceMap = SourceMapPicker.SelectedItem?.ToString() ?? "";
			string targetMap = TargetMapPicker.SelectedItem?.ToString() ?? "";

			string outputPath = Path.Combine(FileSystem.Current.AppDataDirectory,
				$"Remapped_{Path.GetFileName(_selectedFilePath)}");

			RemapVariables values = new()
			{
				MidiPath = _selectedFilePath,
				//OutputFilePath = outputPath,
				SourceMapType = _drumMapTypes[sourceMap],
				TargetMapType = _drumMapTypes[targetMap]
			};

			await _midiMapManager.RemapMidi(values);

			ResultLabel.TextColor = Colors.Green;
			ResultLabel.Text = $"MIDI remapped successfully:\n{Path.GetFileName(outputPath)}";
		}
		catch (Exception ex)
		{
			ResultLabel.TextColor = Colors.Red;
			ResultLabel.Text = $"Error: {ex.Message}";
		}
	}
}
