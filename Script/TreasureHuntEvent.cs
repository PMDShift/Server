using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Server.Network;

namespace Script
{
    public class TreasureHuntEvent : IEvent
    {
        public class TreasureHuntData
        {
        }

        public string Identifier => "treasurehunt";

        public string Name => "Treasure Hunt";

        public string IntroductionMessage => "Treasure has been scattered throughout the overworld! Find it all!";

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

        public void Start()
        {

        }

        public void End()
        {

        }
    }
}
