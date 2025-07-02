using Managers.Contracts;
using Models;

namespace Managers.Implementations;

public class CliArgumentManager : ICliArgumentManager
{
    private const string USAGE = "Usage: <source_map> <target_map> <midi_file>";
    private const string EXAMPLE = "Example: GuitarPro StevenSlate midis/example.mid";
    private const string INSUFFICIENT_ARGS_MESSAGE = "[ERROR] Insufficient arguments. 3 arguments are required.";
    private const string AVAILABLE_MAPS = "Available maps:";

    public Task<RemapVariables> Execute(string[] args)
    {
        if (args.Length < 3)
        {
            Console.WriteLine(USAGE);
            Console.WriteLine(EXAMPLE);
            throw new NullReferenceException(INSUFFICIENT_ARGS_MESSAGE);
        }

        var sourceArg = args[0];
        var targetArg = args[1];
        var midiPath = args[2];

        if (!Enum.TryParse<DrumMapType>(sourceArg, true, out var sourceMapType))
        {
            PrintAvailableMapTypes();
            throw new ArgumentException($"Invalid source map: '{sourceArg}'");
        }

        if (!Enum.TryParse<DrumMapType>(targetArg, true, out var targetMapType))
        {
            PrintAvailableMapTypes();
            throw new ArgumentException($"Invalid target map: '{targetArg}'");
        }

        return Task.Run(() => new RemapVariables
        {
            SourceMapType = sourceMapType,
            TargetMapType = targetMapType,
            MidiPath = midiPath
        });
    }

    private static void PrintAvailableMapTypes()
    {
        Console.WriteLine(AVAILABLE_MAPS);
        foreach (string name in Enum.GetNames<DrumMapType>())
        {
            Console.WriteLine($"- {name}");
        }
    }
}
