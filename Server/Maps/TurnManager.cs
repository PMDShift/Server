using System;
using System.Collections.Generic;
using System.Text;
using Server.Network;

namespace Server.Maps
{
    public class TurnManager
    {
        IMap map;

        public TurnManager(IMap map)
        {
            this.map = map;
        }

        public void CheckTurns()
        {
            foreach (var client in map.GetClients())
            {
                if (!client.Player.CommitmentState.HasPendingCommitment)
                {
                    // Not all players have taken a turn yet
                    return;
                }
            }

            // Everyone has submitted a commitment. Process them.
            // TODO: Sort by speed
            // TODO: Add movement rate limiting

            foreach (var client in map.GetClients())
            {
                client.Player.CommitmentState.PendingCommitment.Apply(client, map);
                client.Player.CommitmentState.CompleteCommitment();
            }

            Messenger.SendTurnCompleteToMap(map);
        }
    }
}
