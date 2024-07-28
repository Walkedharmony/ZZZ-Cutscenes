using System.CommandLine;

namespace ZZZCutscenes;

public class ResetCommand : Command
{
    public ResetCommand()
        : base("reset", "Reset 'appsettings.json' file to default")
    {
        this.SetHandler(Execute);
    }

    private void Execute()
    {
        const string str = """
            {
                "Settings": {
                "MkvMergePath": "",
                "FfmpegPath": ""
                }
            }
            """;
        File.WriteAllText(Path.Combine(AppContext.BaseDirectory, "appsettings.json"), str);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("'appsettings.json' has been reset to default.");
        Console.ResetColor();
    }
}
