# Drum MIDI Remapper

A .NET tool for remapping MIDI drum notes between different standards and custom mappings. Designed to help musicians and producers adapt MIDI drum tracks for compatibility with various drum kits, DAWs, and hardware.

![Build Status](https://img.shields.io/github/actions/workflow/status/Abstractize/drum-midi-remapper/ci.yml?branch=main)
![License](https://img.shields.io/github/license/Abstractize/drum-midi-remapper)

---

## Quick Start

```bash
git clone https://github.com/Abstractize/drum-midi-remapper.git
cd drum-midi-remapper
dotnet build
dotnet run --project src/ClI -- GuitarPro StevenSlate midis/test.mid
```

- Replace `GuitarPro`, `StevenSlate`, and `midis/test.mid` with your desired mapping and MIDI file.

## Requirements

- [.NET 9.0 SDK or newer](https://dotnet.microsoft.com/download)
- Compatible with Windows, macOS, and Linux

## Features

- Remap MIDI drum notes using customizable mapping files
- Support for popular drum mapping standards (e.g., General MIDI, Roland, Yamaha) and custom mappings
- Batch processing of MIDI files
- Command-line interface for easy automation
- Cross-platform support via .NET

## Usage

Run the tool with the following command:

```bash
dotnet run --project src/ClI -- <SourceMap> <TargetMap> <InputMidiFile>
```

- `<SourceMap>` and `<TargetMap>`: Mapping names (see **Available Mappings**)
- `<InputMidiFile>`: Path to your MIDI file

The `--project src/ClI` option tells `dotnet` to run the CLI project directly.

## Available Mappings

Mappings are stored in the **Services/Resources/Maps/** directory. Specify the mapping name as a command-line argument when running the tool.

**Included mappings:**
- GuitarPro
- LogicPro
- ProTools
- StevenSlate

You can use these standards or create your own custom mapping.

## Configuration

Mappings are defined as JSON files in the **Services/Resources/Maps/** directory. You can create or modify mapping files to suit your needs. See the sample files for format and usage.

### Example Mapping File

Example (**Services/Resources/Maps/ExampleMap.json**):

```json
{
    "name": "ExampleMap",
    "mapping": {
        "Kick": 36,
        "Snare": 38,
        "HiHatClosed": 42,
        "HiHatOpen": 46,
        "TomLow": 41,
        "TomMid": 45,
        "TomHigh": 48,
        "Crash": 49,
        "Ride": 51
    }
}
```

Customize the `"name"` and MIDI note numbers as needed for your drum kit or standard.

> **Note:** The numbers correspond to MIDI note numbers as used by the [DryWetMIDI](https://melanchall.github.io/drywetmidi/) library.  
> For a full list of MIDI note numbers and their corresponding drum sounds, refer to the [General MIDI Percussion Key Map](https://www.midi.org/specifications-old/item/gm-level-1-sound-set), your drum kit's documentation, or the [DryWetMIDI documentation](https://melanchall.github.io/drywetmidi/articles/notes.html).

## Example

**Input:** A MIDI file mapped for GuitarPro  
**Command:**  
```bash
dotnet run --project src/ClI -- GuitarPro StevenSlate midis/test.mid
```
**Output:** A MIDI file remapped for StevenSlate drums.

## Troubleshooting & FAQ

- **Build errors:** Ensure you have the correct .NET SDK installed.
- **Mapping not found:** Check the spelling and existence of your mapping file in **Services/Resources/Maps/**.
- **MIDI file issues:** Verify your input file is a valid MIDI file.

## Contributing

Contributions are welcome! Please open issues or submit pull requests.

### Adding a New Mapping

1. Create a new JSON mapping file in the **Services/Resources/Maps/** directory.
2. Add your mapping details following the format shown above.
3. Update the mapping enum in `Models/DrumMapType.cs` to include your new mapping.
4. Submit a pull request with your changes.

This helps keep all mappings organized and available for everyone.

## Related Projects

- [DryWetMIDI](https://melanchall.github.io/drywetmidi/) - MIDI processing library used by this tool

## License

MIT License