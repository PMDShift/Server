using System;
using System.Collections.Generic;
using System.Text;
using Server.Network;

namespace Script.Events
{
    public interface IEvent
    {
        void ConfigurePlayer(Client client);

        void Load(string data);
        string Save();
    }
}
