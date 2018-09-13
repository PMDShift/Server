using PMDCP.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Zones
{
    public class ZoneCollection
    {
        ListPair<int, Zone> zones;

        public ListPair<int, Zone> Zones
        {
            get { return zones; }
        }

        public int Count
        {
            get { return zones.Count; }
        }

        public ZoneCollection()
        {
            zones = new ListPair<int, Zone>();
        }

        public Zone this[int index]
        {
            get { return zones[index]; }
            set { zones[index] = value; }
        }
    }
}
