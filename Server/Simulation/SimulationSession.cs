using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.RDungeons;
using Server.Reviews;

namespace Server.Simulation
{
    public class SimulationSession
    {
        public RDungeons.RDungeon Dungeon { get; }

        public SimulationSession(RDungeons.RDungeon dungeon)
        {
            this.Dungeon = dungeon;
        }

        public Task<Review> RunSimulationAsync()
        {
            var review = new Review();

            var floorReviews = new List<ReviewDelta>();

            for (var i = 0; i < Dungeon.Floors.Count; i++)
            {
                var floorDefinition = Dungeon.Floors[i];

                var floorReview = new ReviewDelta();

                var floor = RDungeonFloorGen.GenerateFloor(null, Dungeon.DungeonIndex, i, floorDefinition.Options);

                foreach (var activeItem in floor.ActiveItem.Enumerate())
                {
                    floorReview.Items.Add(new ReviewedItem(activeItem.Num, activeItem.Value, activeItem.Tag, 100, new RDungeonFloorLocation(false, Dungeon.DungeonIndex, i)));
                }

                floorReviews.Add(floorReview);
            }

            foreach (var floorReview in floorReviews)
            {
                review.Merge(floorReview);
            }

            return Task.FromResult(review);
        }
    }
}
