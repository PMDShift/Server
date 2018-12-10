using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Reviews
{
    public class RDungeonFloorLocation : ILocation
    {
        public int RDungeonIndex { get; set; }
        public HashSet<int> Floors { get; set; }

        public RDungeonFloorLocation(int rdungeonIndex, int floor)
        {
            this.Floors = new HashSet<int>();

            this.RDungeonIndex = rdungeonIndex;
            this.Floors.Add(floor);
        }

        public string GetDescription()
        {
            var rdungeon = Server.RDungeons.RDungeonManager.RDungeons[RDungeonIndex];

            var floorsMessage = string.Join(", ", Floors.Select(x => x + 1));

            return $"RDungeon [{RDungeonIndex + 1}] `{rdungeon.DungeonName}` Floors {floorsMessage}";
        }

        public bool Equals(ILocation other)
        {
            if (other is RDungeonFloorLocation rDungeonFloorLocation)
            {
                if (rDungeonFloorLocation.RDungeonIndex == RDungeonIndex && rDungeonFloorLocation.Floors.SequenceEqual(Floors))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
