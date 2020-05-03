using System;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DiscordCSModels.Sockets
{
    public class PayloadFrame
    {
        public enum Opcodes
        {
            Dispatch = 0,
            Heartbeat = 1,
            Identify = 2,
            PresenceUpdate = 3,
            VoiceStateUpdate = 4,
            Resume = 6,
            Reconnect = 7,
            RequestGuildMembers = 8,
            InvalidSession = 9,
            Hello = 10,
            HeartbeatACK = 11
        }

        [JsonProperty("op")]
        public int Operation { get; set; }
        [JsonProperty("t", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }
        [JsonProperty("s", NullValueHandling = NullValueHandling.Ignore)]
        public int? Sequence { get; set; }
        [JsonProperty("d")]
        public object Payload { get; set; }

        public override string ToString()
        {
            StringBuilder resultBuilder = new StringBuilder();
            resultBuilder.AppendLine($"Operation: {Enum.GetName(typeof(Opcodes), Operation)}");
            resultBuilder.AppendLine($"Type: {Type}");
            resultBuilder.AppendLine($"Sequence: {Sequence}");
            resultBuilder.AppendLine($"Payload: {JsonConvert.SerializeObject(Payload)}");

            return resultBuilder.ToString();
        }
    }
}
