using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.Zones;

namespace Server.Reviews
{
    class RDungeonContentReviewer : IContentReviewer
    {
        public ZoneResourceType Type => ZoneResourceType.RDungeons;

        public ReviewDelta Review(int id)
        {
            var review = new ReviewDelta();

            var rdungeon = Server.RDungeons.RDungeonManager.RDungeons[id];

            var dungeonItems = new List<ReviewedItem>();

            for (var i = 0; i < rdungeon.Floors.Count; i++)
            {
                var floor = rdungeon.Floors[i];

                foreach (var item in floor.Items)
                {
                    dungeonItems.Add(new ReviewedItem(item.ItemNum, item.MaxAmount, item.Tag, item.AppearanceRate, new RDungeonFloorLocation(id, i)));
                }
            }

            foreach (var dungeonItem in dungeonItems)
            {
                var mergedDungeonItem = review.Items.Where(x => x.Number == dungeonItem.Number && x.Amount == dungeonItem.Amount && x.Tag == dungeonItem.Tag).FirstOrDefault();

                if (mergedDungeonItem == null)
                {
                    mergedDungeonItem = dungeonItem;
                    review.Items.Add(dungeonItem);
                }

                var location = (RDungeonFloorLocation)dungeonItem.Location;
                var mergedLocation = (RDungeonFloorLocation)mergedDungeonItem.Location;

                foreach (var dungeonItemFloor in location.Floors)
                {
                    if (!mergedLocation.Floors.Contains(dungeonItemFloor))
                    {
                        mergedLocation.Floors.Add(dungeonItemFloor);
                    }
                }
            }

            return review;
        }
    }
}
