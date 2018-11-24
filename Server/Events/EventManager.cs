using System;
using System.Collections.Generic;
using System.Text;
using Server.Network;

namespace Server.Events
{
    public static class EventManager
    {
        public static HashSet<string> RegisteredCharacters { get; }

        public static string ActiveEventIdentifier { get; set; }

        static string eventData;

        static EventManager()
        {
            RegisteredCharacters = new HashSet<string>();
        }

        public static IEnumerable<Client> GetRegisteredClients()
        {
            foreach (var registeredCharacter in RegisteredCharacters)
            {
                var client = ClientManager.FindClientFromCharID(registeredCharacter);

                if (client != null)
                {
                    yield return client;
                }
            }
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

        public static void SaveEventData(string eventData)
        {
            EventManager.eventData = eventData;
        }

        public static string LoadEventData()
        {
            return EventManager.eventData;
        }
    }
}
