using System;
using System.Text;
using Newtonsoft.Json;

namespace DiscordCSModels.Sockets
{
    public class PayloadFrame
    {
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
            resultBuilder.AppendLine($"Operation: {Operation}");
            resultBuilder.AppendLine($"Type: {Type}");
            resultBuilder.AppendLine($"Sequence: {Sequence}");
            resultBuilder.AppendLine($"Payload: {JsonConvert.SerializeObject(Payload)}");

            return resultBuilder.ToString();
        }
    }
}
