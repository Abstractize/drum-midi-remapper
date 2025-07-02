namespace Models.Tests;

public class RemapVariablesTest
{
    [Fact]
    public void RemapVariables_DefaultConstructor_InitializesProperties()
    {
        var remapVariables = new RemapVariables
        {
            SourceMapType = DrumMapType.GuitarPro,
            TargetMapType = DrumMapType.LogicPro,
            MidiPath = "test.mid"
        };

        Assert.Equal(DrumMapType.GuitarPro, remapVariables.SourceMapType);
        Assert.Equal(DrumMapType.LogicPro, remapVariables.TargetMapType);
        Assert.Equal("test.mid", remapVariables.MidiPath);
    }

    [Fact]
    public void RemapVariables_CanSetSourceMapType()
    {
        var remapVariables = new RemapVariables
        {
            SourceMapType = DrumMapType.LogicPro,
            TargetMapType = DrumMapType.GuitarPro,
            MidiPath = "file.mid"
        };

        remapVariables.SourceMapType = DrumMapType.GuitarPro;

        Assert.Equal(DrumMapType.GuitarPro, remapVariables.SourceMapType);
    }

    [Fact]
    public void RemapVariables_CanSetTargetMapType()
    {
        var remapVariables = new RemapVariables
        {
            SourceMapType = DrumMapType.GuitarPro,
            TargetMapType = DrumMapType.LogicPro,
            MidiPath = "file.mid"
        };

        remapVariables.TargetMapType = DrumMapType.GuitarPro;

        Assert.Equal(DrumMapType.GuitarPro, remapVariables.TargetMapType);
    }

    [Fact]
    public void RemapVariables_CanSetMidiPath()
    {
        var remapVariables = new RemapVariables
        {
            SourceMapType = DrumMapType.GuitarPro,
            TargetMapType = DrumMapType.LogicPro,
            MidiPath = "old.mid"
        };

        remapVariables.MidiPath = "new.mid";

        Assert.Equal("new.mid", remapVariables.MidiPath);
    }
}