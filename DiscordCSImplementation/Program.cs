using System;
using System.Threading.Tasks;
using DiscordCS;

namespace DiscordCSImplementation
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await DiscordCS.Sockets.Handler.Connect();
            string connectResponse = await DiscordCS.Sockets.Handler.Receive();
            Console.WriteLine(connectResponse);
            await DiscordCS.Sockets.Handler.Disconnect();
        }
    }
}
