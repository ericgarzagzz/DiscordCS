using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DiscordCS;
using DiscordCSModels.Sockets;

namespace DiscordCSImplementation
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await DiscordCS.Sockets.Handler.Connect();
            string rawResponse = await DiscordCS.Sockets.Handler.Receive();
            PayloadFrame response = JsonConvert.DeserializeObject<PayloadFrame>(rawResponse);
            Console.WriteLine(response.Operation);
            Console.WriteLine(response.Sequence);
            Console.WriteLine(response.Type);
            Console.WriteLine(JsonConvert.SerializeObject(response.Payload));
            await DiscordCS.Sockets.Handler.Disconnect();
        }
    }
}
