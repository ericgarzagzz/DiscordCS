using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DiscordCS;
using DiscordCSModels.Sockets;
using System.Threading;

namespace DiscordCSImplementation
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await DiscordCS.Sockets.Handler.Connect();
            int heartbeatingInterval = await DiscordCS.Gateways.Heartbeating.GetHeartBeatingInterval();

            DiscordCS.Gateways.Heartbeating.Start(heartbeatingInterval);

            Console.Read();

            await DiscordCS.Sockets.Handler.Disconnect();
        }
    }
}
