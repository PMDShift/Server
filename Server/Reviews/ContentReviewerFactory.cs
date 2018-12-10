using System;
using System.Collections.Generic;
using System.Text;
using Server.Zones;

namespace Server.Reviews
{
    public class ContentReviewerFactory
    {
        public static IContentReviewer CreateReviewer(ZoneResourceType type)
        {
            switch (type)
            {
                case ZoneResourceType.Maps:
                    return new MapContentReviewer();
                case ZoneResourceType.RDungeons:
                    return new RDungeonContentReviewer();
                case ZoneResourceType.NPCs:
                    return new NPCContentReviewer();
                default:
                    return null;
            }
        }
    }
}
