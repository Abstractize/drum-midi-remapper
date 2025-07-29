#if !MACCATALYST

using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Models;

namespace Services.Extensions;

public static class MidiFileExtensions
{
    private static Dictionary<int, int> BuildMapping(DrumMap sourceMap, DrumMap targetMap)
    {
        Dictionary<int, int> mapping = [];

        foreach (var kvp in sourceMap.Mapping)
        {
            var drumName = kvp.Key;
            var sourceNote = kvp.Value;

            if (targetMap.Mapping.TryGetValue(drumName, out int targetNote))
            {
                mapping[sourceNote] = targetNote;
            }
        }

        return mapping;
    }

    public static void RemapNotes(this MidiFile midiFile, DrumMap sourceMap, DrumMap targetMap)
    {
        Dictionary<int, int> mapping = BuildMapping(sourceMap, targetMap);

        foreach (var trackChunk in midiFile.GetTrackChunks())
        {
            foreach (var midiEvent in trackChunk.Events)
            {
                if (midiEvent is NoteOnEvent noteOn)
                {
                    if (mapping.TryGetValue(noteOn.NoteNumber, out int mappedNote))
                    {
                        noteOn.NoteNumber = (SevenBitNumber)mappedNote;
                    }
                }
                else if (midiEvent is NoteOffEvent noteOff)
                {
                    if (mapping.TryGetValue(noteOff.NoteNumber, out int mappedNote))
                    {
                        noteOff.NoteNumber = (SevenBitNumber)mappedNote;
                    }
                }
            }
        }
    }
}

#endif