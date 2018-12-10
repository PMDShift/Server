using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Reviews
{
    public class ReviewDelta
    {
        public List<ReviewedItem> Items { get; }

        public ReviewDelta()
        {
            this.Items = new List<ReviewedItem>();
        }

        public void Merge(ReviewDelta reviewDelta)
        {
            foreach (var otherItem in reviewDelta.Items)
            {
                var matchingItem = Items.Where(x => x.Number == otherItem.Number && x.AppearanceRate == otherItem.AppearanceRate && x.Location.Equals(otherItem.Location)).FirstOrDefault();

                if (matchingItem == null)
                {
                    Items.Add(otherItem);
                } else
                {
                    matchingItem.Amount += otherItem.Amount;
                }
            }
        }
    }
}
