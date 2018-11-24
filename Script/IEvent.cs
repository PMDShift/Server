using System;
using System.Collections.Generic;
using System.Text;
using Server.Network;
using Server.Maps;
using Server.Players;
using Server.Combat;

namespace Script
{
    public interface IEvent
    {
        string Identifier { get; }
        string Name { get; }

        string IntroductionMessage { get; }

        void ConfigurePlayer(Client client);

        void OnActivateMap(IMap map);
        void OnPickupItem(ICharacter character, int itemSlot, InventoryItem invItem);

        void Load(string data);
        string Save();

        void Start();
        void End();
        void AnnounceWinner();
    }
}
