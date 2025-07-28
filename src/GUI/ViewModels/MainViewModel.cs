using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Maui.Storage;
using Managers.Contracts;
using Models;

namespace GUI.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IMidiMapManager _midiManager;

    public MainViewModel(IMidiMapManager midiManager)
    {
        _midiManager = midiManager;
        MapTypes = Enum.GetValues<DrumMapTypes>().ToList();
    }

    [ObservableProperty]
    private DrumMapTypes sourceMap;

    [ObservableProperty]
    private DrumMapTypes targetMap;

    [ObservableProperty]
    private Stream? selectedFile;
    [ObservableProperty]
    private string? fileName;

    public List<DrumMapTypes> MapTypes { get; }


    [RelayCommand]
    private async Task PickFile()
    {
        var options = new PickOptions
        {
            PickerTitle = "Select a MIDI file",
            FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.MacCatalyst, ["public.audio"] },
                    { DevicePlatform.WinUI, [".mid", ".midi"] }
                })
        };

        FileResult? result = await FilePicker.Default.PickAsync(options);
        if (result != null)
        {
            SelectedFile = await result.OpenReadAsync();
            fileName = result.FileName;
        }
        else
        {
            await Application.Current!.Windows[0].Page!.DisplayAlert("Error", "No file selected.", "OK");
        }

    }

    [RelayCommand]
    private async Task Remap()
    {
        try
        {
            if (SelectedFile == null)
            {
                await Application.Current!.Windows[0].Page!.DisplayAlert("Error", "Please select a MIDI file.", "OK");
                return;
            }

            Console.WriteLine($"Source Map: {SourceMap}, Target Map: {TargetMap}");

            Stream tempFile = await _midiManager.RemapMidi(SourceMap.ToString(), TargetMap.ToString(), SelectedFile);

            var saveResult = await FileSaver.Default.SaveAsync("remapped.mid", tempFile);

            if (saveResult.IsSuccessful)
                await Application.Current!.Windows[0].Page!.DisplayAlert("Success", "✅ MIDI remapped and saved!", "OK");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during remapping: {ex.Message}");
            await Application.Current!.Windows[0].Page!.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }

    private static string GetDownloadsPath()
    {
        string downloadsPath = string.Empty;
        if (OperatingSystem.IsWindows())
        {
            downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
        }
        else if (OperatingSystem.IsMacOS())
        {
            downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/Downloads";
        }
        else if (OperatingSystem.IsLinux())
        {
            downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/Downloads";
        }
        else
        {
            throw new PlatformNotSupportedException("Unsupported platform for Downloads path.");
        }

        return downloadsPath;
    }
}