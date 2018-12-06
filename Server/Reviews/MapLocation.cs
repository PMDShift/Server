using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Reviews
{
    public class MapLocation : ILocation
    {
        public int MapNumber { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public MapLocation(int mapNumber, int x, int y)
        {
            this.MapNumber = mapNumber;
            this.X = x;
            this.Y = y;
        }

        public string GetDescription()
        {
            var map = Maps.MapManager.RetrieveMap(MapNumber);

            return $"Map [{MapNumber}] `{map.Name}` ({X}, {Y})";
        }

        public bool Equals(ILocation other)
        {
            if (other is MapLocation mapLocation)
            {
                if (mapLocation.MapNumber == MapNumber && mapLocation.X == X && mapLocation.Y == Y)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
