using System;
using System.Collections.Generic;
using System.Text;
using Server.Network;

namespace Script
{
    public interface IEvent
    {
        string Identifier { get; }
        string Name { get; }

        string IntroductionMessage { get; }

        void ConfigurePlayer(Client client);

        void Load(string data);
        string Save();

        void Start();
        void End();
    }
}
