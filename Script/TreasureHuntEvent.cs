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
            public bool Started { get; set; }

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

        public string IntroductionMessage => "Treasure has been scattered throughout the overworld! Find it all!";

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
            if (Data.Started)
            {
                if (Ranks.IsDisallowed(client, Enums.Rank.Scripter))
                {
                    client.Player.KillableAnywhere = true;
                }
                client.Player.BeginTempStatMode(100, true);
            }
        }

        public void DeconfigurePlayer(Client client)
        {
            client.Player.KillableAnywhere = false;
            client.Player.EndTempStatMode();

            PacketHitList packetHitList = null;
            PacketHitList.MethodStart(ref packetHitList);

            Main.RefreshCharacterSpeedLimit(client.Player.GetActiveRecruit(), client.Player.Map, packetHitList);

            PacketHitList.MethodEnded(ref packetHitList);
        }

        public void OnActivateMap(IMap map)
        {
            foreach (var eventItem in Data.EventItems.Where(x => !x.Claimed && x.MapID == map.MapID))
            {
                var existingItem = map.ActiveItem.Enumerate().Where(x => x.Num == TreasureItemID && x.X == eventItem.X && x.Y == eventItem.Y).FirstOrDefault();

                if (existingItem == null)
                {
                    map.SpawnItem(TreasureItemID, 1, false, false, "", true, eventItem.X, eventItem.Y, null);
                }
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

        public void OnDeath(Client client)
        {
            if (Data.Started)
            {
                var itemCount = client.Player.HasItem(TreasureItemID);

                if (itemCount > 0)
                {
                    var quantityToLose = (int)System.Math.Max(1, itemCount * 0.3f);

                    client.Player.TakeItem(TreasureItemID, quantityToLose);

                    Messenger.GlobalMsg($"{client.Player.DisplayName} lost {quantityToLose} treasure(s)!", Text.BrightGreen);

                    var claimedTreasures = Data.EventItems.Where(x => x.Claimed).ToList();
                    for (var i = 0; i < quantityToLose; i++)
                    {
                        if (claimedTreasures.Count > 0)
                        {
                            var index = Server.Math.Rand(0, claimedTreasures.Count);

                            claimedTreasures[index].Claimed = false;

                            claimedTreasures.RemoveAt(index);
                        }
                    }

                    ActivateTreasures();
                }
            }
        }

        public void Start()
        {
            Data.EventItems = new TreasureHuntData.TreasureData[] {
                new TreasureHuntData.TreasureData() { MapID = "s152", X = 21, Y = 15, Claimed = false },
                new TreasureHuntData.TreasureData() { MapID = "s152", X = 20, Y = 15, Claimed = false },
                new TreasureHuntData.TreasureData() { MapID = "s152", X = 22, Y = 15, Claimed = false },
            };

            Data.Started = true;

            foreach (var client in EventManager.GetRegisteredClients())
            {
                CleanupTreasures(client);
            }

            ActivateTreasures();
        }

        public void End()
        {
            Data.Started = false;

            foreach (var client in EventManager.GetRegisteredClients())
            {
                Messenger.PlayerWarp(client, 152, 15, 16);
            }

            foreach (var eventItem in Data.EventItems)
            {
                var map = MapManager.RetrieveActiveMap(eventItem.MapID);

                if (map != null)
                {
                    for (var i = 0; i < map.ActiveItem.Length; i++)
                    {
                        if (map.ActiveItem[i].Num == TreasureItemID && map.ActiveItem[i].X == eventItem.X && map.ActiveItem[i].Y == eventItem.Y)
                        {
                            map.SpawnItemSlot(i, -1, 0, false, false, "", map.IsZoneOrObjectSandboxed(), eventItem.X, eventItem.Y, null);
                        }
                    }
                }
            }
        }

        private void ActivateTreasures()
        {
            var activatedMaps = new HashSet<string>();
            foreach (var client in EventManager.GetRegisteredClients())
            {
                if (!activatedMaps.Contains(client.Player.MapID))
                {
                    OnActivateMap(client.Player.Map);

                    activatedMaps.Add(client.Player.MapID);
                }
            }
        }

        private void CleanupTreasures(Client client)
        {
            var treasureCount = client.Player.HasItem(TreasureItemID);

            if (treasureCount > 0)
            {
                client.Player.TakeItem(TreasureItemID, treasureCount);
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
                CleanupTreasures(client);

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

