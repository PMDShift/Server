using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Server;
using Server.Network;
using Server.Events;
using Server.Maps;
using System.Linq;
using Server.Combat;
using Server.Players;
using Server.Stories;

namespace Script
{
    public class TreasureHuntEvent : IEvent
    {
        public class TreasureHuntData
        {
            public class TreasureData
            {
                public string MapID { get; set; }
                public int X { get; set; }
                public int Y { get; set; }
                public bool Claimed { get; set; }
            }

            public TreasureData[] EventItems = Array.Empty<TreasureData>();

        }

        public string Identifier => "treasurehunt";

        public string Name => "Treasure Hunt";

        public string IntroductionMessage => "Treasure has been scattered throughout the overworld! Find it all! Kill others to steal their treasure!";

        public TreasureHuntData Data { get; private set; }

        public static readonly int TreasureItemID = 980;

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

        public void OnActivateMap(IMap map)
        {
            foreach (var eventItem in Data.EventItems.Where(x => !x.Claimed && x.MapID == map.MapID))
            {
                map.SpawnItem(TreasureItemID, 1, false, false, "", true, eventItem.X, eventItem.Y, null);
            }
        }

        public void OnPickupItem(ICharacter character, int itemSlot, InventoryItem invItem)
        {
            if (character.CharacterType == Enums.CharacterType.Recruit)
            {
                var player = ((Recruit)character).Owner.Player;

                if (invItem.Num == TreasureItemID)
                {
                    var eventItem = Data.EventItems.Where(x => x.MapID == character.MapID && x.X == character.X && x.Y == character.Y).FirstOrDefault();

                    if (eventItem != null)
                    {
                        eventItem.Claimed = true;

                        var claimedCount = Data.EventItems.Where(x => x.Claimed).Count();

                        Messenger.GlobalMsg($"{player.DisplayName} found some treasure! ({claimedCount}/{Data.EventItems.Length})", Text.BrightGreen);
                    }
                }
            }
        }

        public void Start()
        {
            Data.EventItems = new TreasureHuntData.TreasureData[] {
                new TreasureHuntData.TreasureData() { MapID = "s152", X = 21, Y = 15, Claimed = false }
            };
        }

        public void End()
        {
            foreach (var client in EventManager.GetRegisteredClients())
            {
                Messenger.PlayerWarp(client, 152, 15, 16);
            }
        }

        public void AnnounceWinner()
        {
            var rankings = new List<Tuple<Client, int>>();
            foreach (var client in EventManager.GetRegisteredClients())
            {
                var amount = client.Player.HasItem(TreasureItemID);

                if (amount > 0)
                {
                    rankings.Add(Tuple.Create(client, amount));
                }
            }

            var sortedRankings = rankings.OrderByDescending(x => x.Item2).ToList();

            foreach (var client in EventManager.GetRegisteredClients())
            {
                var story = new Story();
                var segment = StoryBuilder.BuildStory();
                StoryBuilder.AppendSaySegment(segment, $"And the winners are...!", -1, 0, 0);

                if (sortedRankings.Count >= 3)
                {
                    StoryBuilder.AppendSaySegment(segment, "In third place...", -1, 0, 0);
                    StoryBuilder.AppendSaySegment(segment, $"{sortedRankings[2].Item1.Player.DisplayName}, with a score of {sortedRankings[2].Item2}!", -1, 0, 0);
                }

                if (sortedRankings.Count >= 2)
                {
                    StoryBuilder.AppendSaySegment(segment, "In second place...", -1, 0, 0);
                    StoryBuilder.AppendSaySegment(segment, $"{sortedRankings[1].Item1.Player.DisplayName}, with a score of {sortedRankings[1].Item2}!", -1, 0, 0);
                }

                if (sortedRankings.Count >= 1)
                {
                    StoryBuilder.AppendSaySegment(segment, "In first place...", -1, 0, 0);
                    StoryBuilder.AppendSaySegment(segment, $"{sortedRankings[0].Item1.Player.DisplayName}, with a score of {sortedRankings[0].Item2}!", -1, 0, 0);
                }

                if (sortedRankings.Count == 0)
                {
                    StoryBuilder.AppendSaySegment(segment, "...no one. Strange?", -1, 0, 0);
                }

                segment.AppendToStory(story);
                StoryManager.PlayStory(client, story);
            }
        }
    }
}
