using Discord;
using Discord.Commands;

namespace CES.Modules
{
    public class PublicModule : ModuleBase<SocketCommandContext>
    {
        [Command("order")]
        public async Task GetOrderAsync(string text)
        {
            Console.WriteLine($"Received msg: {text}");
        }
    }
}