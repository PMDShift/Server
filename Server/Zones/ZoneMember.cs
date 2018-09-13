using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Zones
{
    public class ZoneMember
    {
        public int ZoneID { get; set; }
        public string CharacterID { get; set; }
        public Enums.ZoneAccess Access { get; set; }
    }
}
