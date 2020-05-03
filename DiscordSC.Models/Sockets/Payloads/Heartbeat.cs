using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordCSModels.Sockets.Payloads
{
    public class Heartbeat
    {
        [JsonProperty("heartbeat_interval")]
        public int Interval { get; set; }
    }
}
