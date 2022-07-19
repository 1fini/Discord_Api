using Discord.WebSocket;
using Discord.Commands;
using Discord;
using Microsoft.Extensions.DependencyInjection;
namespace CES;

public class Program
{
    static void Main(string[] args)
    => new Program().RunBotAsync().GetAwaiter().GetResult();

    private DiscordSocketClient client;
    private DiscordSocketRestClient client2;
    private CommandService command;

    public async Task RunBotAsync()
    {
        
        // You should dispose a service provider created using ASP.NET
        // when you are finished using it, at the end of your app's lifetime.
        // If you use another dependency injection framework, you should inspect
        // its documentation for the best way to do this.
        using (var services = ConfigureServices())
        {
            var client = services.GetRequiredService<DiscordSocketClient>();

            client.Log += LogAsync;
            services.GetRequiredService<CommandService>().Log += LogAsync;

            // Tokens should be considered secret data and never hard-coded.
            // We can read from the environment variable to avoid hard coding.
            await client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("AccessToken"));
            await client.StartAsync();

            // Here we initialize the logic required to register our commands.
            await services.GetRequiredService<CommandHandlingService>().InitializeAsync();

            await Task.Delay(Timeout.Infinite);
        }
    }

    private async Task HandleCommandAsync(SocketMessage pMessage)
    {
        var message = (SocketUserMessage)pMessage;
        if(message == null) return;
        //Console.WriteLine($"Message Reçu: {message}");

        //int argPos = 0;

        //var context = new SocketCommandContext(client, message);
        //var result = await command.ExecuteAsync(context, argPos, null);

        //if (!result.IsSuccess)
        //{
            //await context.Channel.SendMessageAsync(result.ErrorReason);
        //}
    }

    private Task LogAsync(LogMessage log)
    {
        Console.WriteLine(log.ToString());

        return Task.CompletedTask;
    }

    private ServiceProvider ConfigureServices() 
        => new ServiceCollection()
            .AddSingleton<DiscordSocketClient>()
            .AddSingleton<CommandService>()
            .AddSingleton<CommandHandlingService>()
    //        .AddSingleton<HttpClient>()
            .BuildServiceProvider();
}

