using DSharpPlus;
using DSharpPlus.CommandsNext;
using VoidAnimeInfoBot;

public class Program
{
    public static void Main(string[] args) => AsyncMain().GetAwaiter().GetResult();

    public static async Task AsyncMain()
    {
        await Users.Initialize();

        var discord = new DiscordClient(new DiscordConfiguration()
        {
            Token = Environment.GetEnvironmentVariable("VOIDANIMEINFOTOKEN", EnvironmentVariableTarget.User),
            TokenType = TokenType.Bot
        });

        var commands = discord.UseCommandsNext(new CommandsNextConfiguration()
        {
            StringPrefixes = new []{ "va_", "v_" }
        });

        commands.RegisterCommands<AnimeInfoModule>();

        await discord.ConnectAsync();

        await Task.Delay(Timeout.Infinite);
    }
}