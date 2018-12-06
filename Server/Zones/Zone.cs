using System;
using System.Collections.Generic;
using System.Text;
using Server.Database;

namespace Server.Zones
{
    public class Zone
    {
        public int Num { get; set; }
        public string Name { get; set; }
        public bool IsOpen { get; set; }
        public ulong DiscordChannelID { get; set; }
        public List<ZoneMember> Members { get; }

        public bool AllowVisitors { get; set; }

        public Zone()
        {
            Members = new List<ZoneMember>();
        }

        public List<ZoneResource> LoadResources()
        {
            var zoneResources = new List<ZoneResource>();

            using (var dbConnection = new DatabaseConnection(DatabaseID.Data))
            {
                zoneResources.AddRange(Dungeons.DungeonManager.LoadZoneResources(dbConnection.Database, Num));
                zoneResources.AddRange(Items.ItemManager.LoadZoneResources(dbConnection.Database, Num));
                zoneResources.AddRange(Maps.MapManager.LoadZoneResources(dbConnection.Database, Num));
                zoneResources.AddRange(Npcs.NpcManager.LoadZoneResources(dbConnection.Database, Num));
                zoneResources.AddRange(RDungeons.RDungeonManager.LoadZoneResources(dbConnection.Database, Num));
                zoneResources.AddRange(Shops.ShopManager.LoadZoneResources(dbConnection.Database, Num));
                zoneResources.AddRange(Stories.StoryManager.LoadZoneResources(dbConnection.Database, Num));
            }

            return zoneResources;
        }
    }
}
