using System;
using System.Threading.Tasks;
using System.Reactive;
using DiscordCS.Gateways;
using DiscordCSModels.Sockets;
using System.Reactive.Linq;

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
            Console.WriteLine("SEND");
            await Gateways.Handler.Send(sendingBeat);
            Console.WriteLine(sendingBeat.ToString());
            Console.WriteLine("RECEIVE");
            PayloadFrame receivingBeat = await Gateways.Handler.Receive();
            Console.WriteLine(receivingBeat.ToString());
        }

        private static PayloadFrame GenerateBeatPayload()
        {
            return new PayloadFrame
            {
                Operation = 1,
                Sequence = lastSequence
            };
        }

        public static IDisposable GetSuscription()
        {
            if (_suscription == null) throw new Exception("Disposable has not been initialized.");
            else return _suscription;
        }
    }
}
