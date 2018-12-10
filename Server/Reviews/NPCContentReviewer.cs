using System;
using System.Collections.Generic;
using System.Text;
using Server.Zones;

namespace Server.Reviews
{
    public class NPCContentReviewer : IContentReviewer
    {
        public ZoneResourceType Type => ZoneResourceType.NPCs;

        public ReviewDelta Review(int id)
        {
            var review = new ReviewDelta();

            var npc = Server.Npcs.NpcManager.Npcs[id];

            for (var i = 0; i < npc.Drops.Length; i++)
            {
                var drop = npc.Drops[i];

                if (drop.ItemNum > 0)
                {
                    review.Items.Add(new ReviewedItem(drop.ItemNum, drop.ItemValue, drop.Tag, drop.Chance, new NPCDropLocation(id, i)));
                }
            }

            return review;
        }
    }
}
