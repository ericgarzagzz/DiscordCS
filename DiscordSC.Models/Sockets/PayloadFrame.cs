using System;
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
    }
}
