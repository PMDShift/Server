using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Zones
{
    public class Zone
    {
        public int Num { get; set; }
        public string Name { get; set; }
        public bool IsOpen { get; set; }
        public ulong DiscordChannelID { get; set; }
        public List<ZoneMember> Members { get; }

        public bool AllowVisitors { get; set; }

        public Zone()
        {
            Members = new List<ZoneMember>();
        }
    }
}
