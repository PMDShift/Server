using System;
using System.Collections.Generic;
using System.Text;
using Server.Maps;
using Server.Network;

namespace Server.Players.Turns
{
    public interface ICommitment
    {
        CommitmentType Type { get; }
        void Apply(Client client, IMap map);
    }
}
