using System;
using System.Collections.Generic;
using System.Text;
using Server.Zones;

namespace Server.Reviews
{
    public class Reviewer
    {
        public Review ReviewZone(int zoneId)
        {
            var zone = ZoneManager.Zones[zoneId];

            var resources = zone.LoadResources();

            var review = new Review();

            foreach (var resource in resources)
            {
                var reviewer = ContentReviewerFactory.CreateReviewer(resource.Type);

                if (reviewer != null)
                {
                    var reviewDelta = reviewer.Review(resource.Num);

                    review.Merge(reviewDelta);
                }
            }

            return review;
        }
    }
}
