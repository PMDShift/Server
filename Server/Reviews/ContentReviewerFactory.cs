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
                default:
                    return null;
            }
        }
    }
}
