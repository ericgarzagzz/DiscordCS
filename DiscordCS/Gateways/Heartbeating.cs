using System;
using System.Threading.Tasks;
using System.Reactive;
using DiscordCS.Gateways;
using DiscordCSModels.Sockets;
using DiscordCSModels.Sockets.Payloads;
using System.Reactive.Linq;
using Newtonsoft.Json;

namespace DiscordCS.Gateways
{
    public static class Heartbeating
    {
        private static IDisposable _suscription;

        public static int? lastSequence = null;

        public static IDisposable Start(int heartbeatInterval)
        {
            _suscription = Observable
                .Interval(TimeSpan.FromMilliseconds(heartbeatInterval))
                .Select(l => Observable.FromAsync(() => Beat(l)))
                .Concat()
                .Subscribe();

            return _suscription;
        }

        private static async Task Beat(long count)
        {
            PayloadFrame sendingBeat = GenerateBeatPayload();
            await Gateways.Handler.Send(sendingBeat);

            PayloadFrame receivingBeat = await Gateways.Handler.Receive();

            if (receivingBeat.Operation != (int)PayloadFrame.Opcodes.Heartbeat)
            {
                _suscription.Dispose();
            }
        }

        private static PayloadFrame GenerateBeatPayload()
        {
            return new PayloadFrame
            {
                Operation = (int)PayloadFrame.Opcodes.HeartbeatACK,
                Sequence = lastSequence
            };
        }

        public static IDisposable GetSuscription()
        {
            if (_suscription == null) throw new Exception("Disposable has not been initialized.");
            else return _suscription;
        }

        public static async Task<int> GetHeartBeatingInterval()
        {
            PayloadFrame frame = await Gateways.Handler.Receive();
            string heartbeatJSON = JsonConvert.SerializeObject(frame.Payload);
            Heartbeat heartbeat = JsonConvert.DeserializeObject<Heartbeat>(heartbeatJSON);

            return heartbeat.Interval;
        }
    }
}
