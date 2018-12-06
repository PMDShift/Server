using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Reviews
{
    public class ReviewedItem
    {
        public int Number { get; set; }
        public int Amount { get; set; }
        public string Tag { get; set; }
        public ILocation Location { get; set; }

        public ReviewedItem(int number, int amount, string tag, ILocation location)
        {
            this.Number = number;
            this.Amount = amount;
            this.Location = location;
        }
    }
}
