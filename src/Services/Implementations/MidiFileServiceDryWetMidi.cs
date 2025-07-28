#if !MACCATALYST
using Melanchall.DryWetMidi.Core;
using Models;
using Services.Contracts;
using Services.Extensions;
namespace Services.Implementations
{
    public class MidiFileServiceDryWetMidi : IMidiFileService
    {
        public async Task<Stream> RemapAsync(DrumMap sourceMap, DrumMap targetMap, Stream midiStream)
        {
            MidiFile midiFile = await ReadMidiAsync(midiStream);

            midiFile.RemapNotes(sourceMap, targetMap);

            var outputStream = new MemoryStream();

            await Task.Run(() =>
            {
                midiFile.Write(outputStream);
            });

            outputStream.Position = 0;

            Console.WriteLine($"Remapped MIDI processed in memory.");

            return outputStream;
        }

        private static async Task<MidiFile> ReadMidiAsync(Stream stream)
        {
            return await Task.Run(() =>
            {
                return MidiFile.Read(stream);
            });
        }
    }
}
#endif