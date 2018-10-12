using System;
using System.Collections.Generic;
using System.Text;
using Server.AI;
using Server.Maps;
using Server.Network;

namespace Server.Players.Turns
{
    public class StepsCommitment : ICommitment
    {
        public CommitmentType Type => CommitmentType.Steps;

        public IReadOnlyList<Enums.Direction> Path { get; }

        public StepsCommitment(IReadOnlyList<Enums.Direction> path)
        {
            this.Path = path;
        }

        public void Apply(Client client, IMap map)
        {
            foreach (var direction in Path)
            {
                var result = MovementProcessor.ProcessMovement(client, direction, client.Player.SpeedLimit, false, true);

                if (!result)
                {
                    // Short-circuit the commitment - the movement was interupted
                    break;
                }
            }
        }
    }
}
