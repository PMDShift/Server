using System;
using System.Collections.Generic;
using System.Text;
using Server.Network;

namespace Server.Events
{
    public static class EventManager
    {
        public static HashSet<string> RegisteredCharacters { get; }

        static EventManager()
        {
            RegisteredCharacters = new HashSet<string>();
        }

        public static void RegisterCharacter(Client client)
        {
            if (!RegisteredCharacters.Contains(client.Player.CharID))
            {
                RegisteredCharacters.Add(client.Player.CharID);
            }
        }

        public static bool IsRegistered(Client client)
        {
            return RegisteredCharacters.Contains(client.Player.CharID);
        }
    }
}
