namespace Models;

public record RemapVariables
{
    public DrumMapType SourceMapType { get; set; }
    public DrumMapType TargetMapType { get; set; }
    public required string MidiPath { get; set; }
}
