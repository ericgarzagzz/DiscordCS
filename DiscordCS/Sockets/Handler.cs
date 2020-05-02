using System;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DiscordCS.Sockets
{
    public class Handler
    {
        private const string DISCORD_URI_STRING = "wss://gateway.discord.gg/?v=6&encoding=json";
        private static Uri _DiscordURI = new Uri(DISCORD_URI_STRING);

        private static ClientWebSocket _client = new ClientWebSocket();

        public static async Task<ClientWebSocket> Connect()
        {
            if (_client.State != WebSocketState.Open)
            {
                await _client.ConnectAsync(_DiscordURI, CancellationToken.None);
            }

            return _client;
        }

        public static async Task Disconnect()
        {
            if (_client.State != WebSocketState.Closed)
            {
                await _client.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
            }
        }

        public static async Task<string> Receive()
        {
            string response = string.Empty;

            var buffer = new ArraySegment<byte>(new byte[2048]);
            do
            {
                WebSocketReceiveResult result;
                using (var ms = new MemoryStream())
                {
                    do
                    {
                        result = await _client.ReceiveAsync(buffer, CancellationToken.None);
                        ms.Write(buffer.Array, buffer.Offset, result.Count);
                    } while (!result.EndOfMessage);

                    if (result.MessageType == WebSocketMessageType.Close)
                        break;

                    ms.Seek(0, SeekOrigin.Begin);
                    using (var reader = new StreamReader(ms, Encoding.UTF8))
                    {
                        return await reader.ReadToEndAsync();
                    }
                }
            } while (true);

            return response;
        }
    }
}
