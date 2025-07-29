namespace Models;

public record RemapVariables
{
    public required string SourceMapType { get; set; }
    public required string TargetMapType { get; set; }
    public required string MidiPath { get; set; }
}
