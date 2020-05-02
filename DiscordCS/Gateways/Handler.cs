using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DiscordCSModels.Sockets;

namespace DiscordCS.Gateways
{
    public class Handler
    {
        public static async Task<PayloadFrame> Receive()
        {
            string rawResponse = await Sockets.Handler.Receive();
            PayloadFrame response = JsonConvert.DeserializeObject<PayloadFrame>(rawResponse);

            return response;
        }

        public static async Task Send(PayloadFrame frame)
        {
            string data = JsonConvert.SerializeObject(frame);
            await Sockets.Handler.Send(data);
        }
    }
}
