using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Reviews
{
    public class RDungeonFloorLocation : ILocation
    {
        public bool DisplayDungeonName { get; }
        public int RDungeonIndex { get; set; }
        public HashSet<int> Floors { get; set; }

        public RDungeonFloorLocation(bool displayDungeonName, int rdungeonIndex, int floor)
        {
            this.Floors = new HashSet<int>();
            this.DisplayDungeonName = displayDungeonName;
            this.RDungeonIndex = rdungeonIndex;

            this.Floors.Add(floor);
        }

        public string GetDescription()
        {
            var floorsMessage = string.Join(", ", Floors.Select(x => x + 1));

            if (DisplayDungeonName)
            {
                var rdungeon = Server.RDungeons.RDungeonManager.RDungeons[RDungeonIndex];

                return $"RDungeon [{RDungeonIndex + 1}] `{rdungeon.DungeonName}` Floors {floorsMessage}";
            } else
            {
                return $"Floors {floorsMessage}";
            }
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
