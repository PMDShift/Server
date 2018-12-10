using System;
using System.Collections.Generic;
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

            for (var i = 0; i < rdungeon.Floors.Count; i++)
            {
                var floor = rdungeon.Floors[i];

                foreach (var item in floor.Items)
                {
                    review.Items.Add(new ReviewedItem(item.ItemNum, item.MaxAmount, item.Tag, item.AppearanceRate, new RDungeonFloorLocation(id, i)));
                }
            }

            return review;
        }
    }
}
