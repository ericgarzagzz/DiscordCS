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

            DiscordCS.Gateways.Heartbeating.Start(5000);

            Thread.Sleep(1000000);

            await DiscordCS.Sockets.Handler.Disconnect();
        }
    }
}
