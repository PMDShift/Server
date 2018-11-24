using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Server.Network;

namespace Script.Events
{
    public class TreasureHuntEvent : IEvent
    {
        public class TreasureHuntData
        {
        }

        public TreasureHuntData Data { get; private set; }

        public TreasureHuntEvent()
        {
            this.Data = new TreasureHuntData();
        }

        public void Load(string data)
        {
            this.Data = JsonConvert.DeserializeObject<TreasureHuntData>(data);
        }

        public string Save()
        {
            return JsonConvert.SerializeObject(Data);
        }

        public void ConfigurePlayer(Client client)
        {
        }
    }
}
