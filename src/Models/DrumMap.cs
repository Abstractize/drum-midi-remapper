namespace Models;

public class DrumMap
{
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Mapa que relaciona un nombre de instrumento con su número de nota MIDI.
    /// Ejemplo: "Kick" => 36
    /// </summary>
    public Dictionary<string, int> Mapping { get; set; } = new();
}