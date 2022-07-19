using Discord.WebSocket;
using Discord;

namespace CES
{
    public class Program
    {
        static void Main(string[] args)
        => new Program().RunBotAsync().GetAwaiter().GetResult();

        private DiscordSocketClient client;

        public async Task RunBotAsync()
        {
            client = new DiscordSocketClient();
            client.Ready += () => 
            {
                Console.WriteLine("Je suis prêt");
                return Task.CompletedTask;
            };

            await client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("AccessToken",EnvironmentVariableTarget.Process));
            await client.StartAsync();

            await Task.Delay(-1);
        }
    }
}

