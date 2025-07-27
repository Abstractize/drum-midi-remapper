namespace Models.Tests;

public class RemapVariablesTest
{
    [Fact]
    public void RemapVariables_DefaultConstructor_InitializesProperties()
    {
        var remapVariables = new RemapVariables
        {
            SourceMapType = DrumMapTypes.GuitarPro.ToString(),
            TargetMapType = DrumMapTypes.LogicPro.ToString(),
            MidiPath = "test.mid"
        };

        Assert.Equal(DrumMapTypes.GuitarPro.ToString(), remapVariables.SourceMapType);
        Assert.Equal(DrumMapTypes.LogicPro.ToString(), remapVariables.TargetMapType);
        Assert.Equal("test.mid", remapVariables.MidiPath);
    }

    [Fact]
    public void RemapVariables_CanSetSourceMapType()
    {
        var remapVariables = new RemapVariables
        {
            SourceMapType = DrumMapTypes.LogicPro.ToString(),
            TargetMapType = DrumMapTypes.GuitarPro.ToString(),
            MidiPath = "file.mid"
        };

        remapVariables.SourceMapType = DrumMapTypes.GuitarPro.ToString();

        Assert.Equal(DrumMapTypes.GuitarPro.ToString(), remapVariables.SourceMapType);
    }

    [Fact]
    public void RemapVariables_CanSetTargetMapType()
    {
        var remapVariables = new RemapVariables
        {
            SourceMapType = DrumMapTypes.GuitarPro.ToString(),
            TargetMapType = DrumMapTypes.LogicPro.ToString(),
            MidiPath = "file.mid"
        };

        remapVariables.TargetMapType = DrumMapTypes.GuitarPro.ToString();

        Assert.Equal(DrumMapTypes.GuitarPro.ToString(), remapVariables.TargetMapType);
    }

    [Fact]
    public void RemapVariables_CanSetMidiPath()
    {
        var remapVariables = new RemapVariables
        {
            SourceMapType = DrumMapTypes.GuitarPro.ToString(),
            TargetMapType = DrumMapTypes.LogicPro.ToString(),
            MidiPath = "old.mid"
        };

        remapVariables.MidiPath = "new.mid";

        Assert.Equal("new.mid", remapVariables.MidiPath);
    }
}