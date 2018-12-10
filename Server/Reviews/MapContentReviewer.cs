using System;
using System.Collections.Generic;
using System.Text;
using Server.Maps;
using Server.Zones;

namespace Server.Reviews
{
    public class MapContentReviewer : IContentReviewer
    {
        public ZoneResourceType Type => ZoneResourceType.Maps;

        public ReviewDelta Review(int id)
        {
            var review = new ReviewDelta();

            var map = MapManager.RetrieveMap(id);

            for (var x = 0; x <= map.MaxX; x++)
            {
                for (var y = 0; y <= map.MaxY; y++)
                {
                    var tile = map.Tile[x, y];

                    switch (tile.Type)
                    {
                        case Enums.TileType.Item:
                            {
                                review.Items.Add(new ReviewedItem(tile.Data1, tile.Data2, tile.String2, 100, new MapLocation(id, x, y)));
                            }
                            break;
                    }
                }
            }

            return review;
        }
    }
}
