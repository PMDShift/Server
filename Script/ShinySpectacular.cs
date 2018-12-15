using System;
using System.Collections.Generic;
using System.Text;
using Server;
using Server.Combat;
using Server.Events;
using Server.Maps;
using Server.Network;
using Server.Players;

namespace Script
{
    public class ShinySpectacular : AbstractEvent<ShinySpectacular.ShinySpectacularData>
    {
        public class ShinySpectacularData : AbstractEventData
        {
            public Dictionary<string, int> Scores { get; set; }
        }

        public override string Identifier => "shinyspectacular";

        public override string Name => "Shiny Spectacular";

        public override string IntroductionMessage => "Swarms of shiny Pokemon have been spotted in dungeons! Defeat the most to win!";

        protected override List<EventRanking> DetermineRankings()
        {
            var rankings = new List<EventRanking>();

            foreach (var client in EventManager.GetRegisteredClients())
            {
                if (Data.Scores.TryGetValue(client.Player.CharID, out var score))
                {
                    rankings.Add(new EventRanking(client, score));
                }
            }

            return rankings;
        }

        public override void OnNpcSpawn(IMap map, MapNpcPreset npc, MapNpc spawnedNpc, PacketHitList hitlist)
        {
            base.OnNpcSpawn(map, npc, spawnedNpc, hitlist);

            if (Data.Started)
            {
                spawnedNpc.Unrecruitable = true;
                spawnedNpc.Shiny = Server.Enums.Coloration.Shiny;
            }
        }

        public override void OnNpcDeath(PacketHitList hitlist, ICharacter attacker, MapNpc npc)
        {
            base.OnNpcDeath(hitlist, attacker, npc);

            if (Data.Started)
            {
                if (attacker.CharacterType == Enums.CharacterType.Recruit)
                {
                    var owner = ((Recruit)attacker).Owner;

                    if (npc.Shiny == Enums.Coloration.Shiny)
                    {
                        if (Data.Scores.ContainsKey(owner.Player.CharID))
                        {
                            Data.Scores[owner.Player.CharID] = Data.Scores[owner.Player.CharID] + 1;
                        }
                        else
                        {
                            Data.Scores.Add(owner.Player.CharID, 1);
                        }
                    }
                }
            }
        }
    }
}
