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
            // https://codereview.stackexchange.com/a/90074
            var processedFloors = Floors.Select(x => x + 1)
                                        .Select((n, i) => new { number = n, group = n - i })
                                        .GroupBy(n => n.group)
                                        .Select(g =>
                                            g.Count() >= 3 ?
                                              g.First().number + "-" + g.Last().number
                                            :
                                              String.Join(", ", g.Select(x => x.number))
                                        )
                                        .ToList();

            var floorsMessage = string.Join(", ", processedFloors);

            if (DisplayDungeonName)
            {
                var rdungeon = Server.RDungeons.RDungeonManager.RDungeons[RDungeonIndex];

                return $"RDungeon [{RDungeonIndex + 1}] `{rdungeon.DungeonName}` Floors {floorsMessage}";
            }
            else
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
