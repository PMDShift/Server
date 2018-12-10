using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Reviews
{
    public class RDungeonFloorLocation : ILocation
    {
        public int RDungeonIndex { get; set; }
        public int Floor { get; set; }

        public RDungeonFloorLocation(int rdungeonIndex, int floor)
        {
            this.RDungeonIndex = rdungeonIndex;
            this.Floor = floor;
        }

        public string GetDescription()
        {
            var rdungeon = Server.RDungeons.RDungeonManager.RDungeons[RDungeonIndex];

            return $"RDungeon [{RDungeonIndex + 1}] `{rdungeon.DungeonName}` Floor {Floor + 1}";
        }

        public bool Equals(ILocation other)
        {
            if (other is RDungeonFloorLocation rDungeonFloorLocation)
            {
                if (rDungeonFloorLocation.RDungeonIndex == RDungeonIndex && rDungeonFloorLocation.Floor == Floor)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
