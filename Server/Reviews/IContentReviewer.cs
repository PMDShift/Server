using System;
using System.Collections.Generic;
using System.Text;
using Server.Zones;

namespace Server.Reviews
{
    public interface IContentReviewer
    {
        ZoneResourceType Type { get; }

        ReviewDelta Review(int id);
    }
}
