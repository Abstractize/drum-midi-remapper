using Melanchall.DryWetMidi.Core;
using Models;
using Services.Contracts;
using Services.Extensions;

namespace Services.Implementations;

public class MidiFileService : IMidiFileService
{
    public async Task<string> RemapAsync(DrumMap sourceMap, DrumMap targetMap, string midiFilePath)
    {
        if (!File.Exists(midiFilePath))
            throw new FileNotFoundException($"MIDI file not found: {midiFilePath}");

        MidiFile midiFile = await ReadMidiAsync(midiFilePath);

        midiFile.RemapNotes(sourceMap, targetMap);

        string fileName = Path.GetFileName(midiFilePath);
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

        await WriteMidiAsync(midiFile, outputPath);

        Console.WriteLine($"Remapped MIDI file saved at: {outputPath}");

        return outputPath;
    }

    private static async Task<MidiFile> ReadMidiAsync(string path)
    {
        return await Task.Run(() =>
        {
            return MidiFile.Read(path);
        });
    }

    private static async Task WriteMidiAsync(MidiFile midi, string path)
    {
        await Task.Run(() =>
        {
            midi.Write(path);
        });
    }
}