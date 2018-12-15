using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Reviews
{
    public class ReviewedNPC
    {
        public string Group { get; set; }

        public int Number { get; set; }

        public ReviewedNPC(int number)
        {
            this.Number = number;
        }
    }
}
