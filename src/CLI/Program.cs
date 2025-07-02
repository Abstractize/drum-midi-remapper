using Microsoft.Extensions.DependencyInjection;
using Managers.Contracts;
using Services;
using Managers;

namespace CLI;

internal class Program
{
    private const string SUCCESS_MESSAGE = "✅ MIDI file remapped successfully.";

    private static async Task Main(string[] args)
    {
        ServiceCollection services = new();

        services.AddServices();
        services.AddManagers();

        ServiceProvider provider = services.BuildServiceProvider();

        ICliArgumentManager argumentManager = provider.GetRequiredService<ICliArgumentManager>();
        IMidiMapManager manager = provider.GetRequiredService<IMidiMapManager>();

        try
        {
            Models.RemapVariables variables = await argumentManager.Execute(args);

            await manager.RemapMidi(variables);
            Console.WriteLine(SUCCESS_MESSAGE);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error: {ex.Message}");
        }
    }
}
