# Drum MIDI Remapper

A **.NET 9** cross-platform tool for remapping MIDI drum notes between different standards and custom mappings. Designed to help musicians and producers adapt MIDI drum tracks for compatibility with various drum kits, DAWs, and hardware.

![Build Status](https://img.shields.io/github/actions/workflow/status/Abstractize/drum-midi-remapper/ci.yml?branch=main)
![License](https://img.shields.io/github/license/Abstractize/drum-midi-remapper)

---

## Quick Start

```bash
git clone https://github.com/Abstractize/drum-midi-remapper.git
cd drum-midi-remapper
dotnet build
dotnet run --project src/CLI -- GuitarPro StevenSlate midis/test.mid
```

- Replace `GuitarPro`, `StevenSlate`, and `midis/test.mid` with your desired mappings and MIDI file.

---

## Requirements

- [.NET 9.0 SDK or newer](https://dotnet.microsoft.com/download)
- Compatible with Windows, macOS, and Linux

---

## Features

- Remap MIDI drum notes using customizable JSON mapping files  
- Support for popular drum mapping standards (e.g., GuitarPro, StevenSlate, LogicPro, ProTools)  
- Batch processing of MIDI files via CLI  
- Cross-platform support powered by .NET 9  
- Modular architecture with Dependency Injection for easy extensibility

---

## Usage

Run the tool with:

```bash
dotnet run --project src/CLI -- <SourceMap> <TargetMap> <InputMidiFile>
```

- `<SourceMap>` and `<TargetMap>`: Mapping names (see **Available Mappings** below)  
- `<InputMidiFile>`: Path to the MIDI file to remap

The `--project src/CLI` option specifies the CLI project.

---

## Available Mappings

Mappings are stored as JSON files in the **Services/Resources/Maps/** directory. Specify the mapping name as a command-line argument when running the tool.

Included mappings:

- GuitarPro  
- LogicPro  
- ProTools  
- StevenSlate

You can also create custom mappings by adding JSON files to the directory.

---

## Configuration

Mapping files are JSON documents located in **Services/Resources/Maps/**. Edit or add files to customize drum note mappings.

### Example Mapping File (`ExampleMap.json`)

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

- `"name"`: Identifier for the map  
- Values correspond to MIDI note numbers

> **Note:** MIDI note numbers follow the [General MIDI Percussion Key Map](https://www.midi.org/specifications-old/item/gm-level-1-sound-set). This project uses [DryWetMIDI](https://melanchall.github.io/drywetmidi/) for MIDI handling.

---

## Example

**Input:** MIDI file mapped for GuitarPro  
**Command:**

```bash
dotnet run --project src/CLI -- GuitarPro StevenSlate midis/test.mid
```

**Output:** MIDI file remapped for StevenSlate drums, saved in the current working directory.

---

## Troubleshooting & FAQ

- **Build errors:** Ensure .NET 9 SDK is installed and your environment is configured correctly.  
- **Mapping not found:** Verify spelling and that JSON mapping files exist in **Services/Resources/Maps/**.  
- **MIDI file issues:** Confirm your input file is a valid MIDI file and accessible.

---

## Contributing

Contributions are welcome! Please open issues or submit pull requests.

### Adding a New Mapping

1. Add a JSON mapping file to **Services/Resources/Maps/**  
2. Follow the existing file format for your mapping  
3. Update the `DrumMapType` enum in `Models/DrumMapType.cs` to include your new map  
4. Submit a pull request

---

## Related Projects

- [DryWetMIDI](https://melanchall.github.io/drywetmidi/) — MIDI processing library used by this tool

---

## License

MIT License
