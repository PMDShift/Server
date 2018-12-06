using PMDCP.Core;
using PMDCP.DatabaseConnector;
using Server.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Zones
{
    public class ZoneManager
    {
        public static event EventHandler LoadComplete;
        public static event EventHandler<LoadingUpdateEventArgs> LoadUpdate;

        public static ZoneCollection Zones { get; private set; }

        public static void Initialize()
        {
            Zones = new ZoneCollection();
        }

        public static void LoadZones(Object object1)
        {
            using (var dbConnection = new DatabaseConnection(DatabaseID.Data))
            {
                string query = "SELECT COUNT(num) FROM zone";
                var row = dbConnection.Database.RetrieveRow(query);

                int count = row["COUNT(num)"].ValueString.ToInt();
                for (int i = 0; i < count; i++)
                {
                    try
                    {
                        LoadZone(i, dbConnection.Database);
                        if (LoadUpdate != null)
                            LoadUpdate(null, new LoadingUpdateEventArgs(i, count - 1));
                    }
                    catch (Exception ex)
                    {
                        Exceptions.ErrorLogger.WriteToErrorLog(ex, "Loading zone #" + i.ToString());
                    }
                }
                if (LoadComplete != null)
                    LoadComplete(null, null);
            }
        }

        public static void LoadZone(int zoneNum, PMDCP.DatabaseConnector.MySql.MySql database)
        {
            var zone = new Zone()
            {
                Num = zoneNum
            };

            var query = "SELECT name, " +
                        "is_open, " +
                        "discord_channel_id," +
                        "allow_visitors " +
                        "FROM zone WHERE zone.num = \'" + zoneNum + "\'";

            var row = database.RetrieveRow(query);
            if (row != null)
            {
                zone.Name = row["name"].ValueString;
                zone.IsOpen = row["is_open"].ValueString.ToBool();
                zone.DiscordChannelID = row["discord_channel_id"].ValueString.ToUlng();
                zone.AllowVisitors = row["allow_visitors"].ValueString.ToBool();
            }

            query = "SELECT " +
                    "character_id, " +
                    "access " +
                    "FROM zone_member WHERE zone_member.zone_id = \'" + zoneNum + "\'";
            foreach (var memberRow in database.RetrieveRowsEnumerable(query))
            {
                var zoneMember = new ZoneMember();
                zoneMember.ZoneID = zoneNum;
                zoneMember.CharacterID = memberRow["character_id"].ValueString;
                zoneMember.Access = (Enums.ZoneAccess)memberRow["access"].ValueString.ToInt();

                zone.Members.Add(zoneMember);
            }

            Zones.Zones.Add(zoneNum, zone);
        }

        public static void CreateZone(Zone zone)
        {
            var zoneNum = Zones.Zones.Count;

            zone.Num = zoneNum;

            Zones.Zones.Add(zoneNum, zone);

            SaveZone(zoneNum);
        }

        public static void SaveZone(int zoneNum)
        {
            if (!Zones.Zones.ContainsKey(zoneNum))
            {
                return;
            }

            var zone = Zones.Zones[zoneNum];

            using (var dbConnection = new DatabaseConnection(DatabaseID.Data))
            {
                var database = dbConnection.Database;
                database.BeginTransaction();

                database.DeleteRow("zone", "num = @Num", new { Num = zoneNum });
                database.DeleteRow("zone_member", "zone_id = @ZoneID", new { ZoneID = zoneNum });

                database.UpdateOrInsert("zone", new IDataColumn[]
                {
                    database.CreateColumn(false, "num", zoneNum.ToString()),
                    database.CreateColumn(false, "name", zone.Name),
                    database.CreateColumn(false, "is_open", zone.IsOpen.ToIntString()),
                    database.CreateColumn(false, "discord_channel_id", zone.DiscordChannelID.ToString()),
                    database.CreateColumn(false, "allow_visitors", zone.AllowVisitors.ToIntString())
                });

                for (var i = 0; i < zone.Members.Count; i++)
                {
                    var zoneMember = zone.Members[i];

                    database.UpdateOrInsert("zone_member", new IDataColumn[]
                    {
                        database.CreateColumn(false, "zone_id", zoneNum.ToString()),
                        database.CreateColumn(false, "character_id", zoneMember.CharacterID.ToString()),
                        database.CreateColumn(false, "access", ((int)zoneMember.Access).ToString())
                    });
                }

                database.EndTransaction();
            }
        }

        public static List<ZoneResource> AddResources(int id, ZoneResourceType resourceType, int amount)
        {
            var results = new List<ZoneResource>();

            if (!ZoneManager.Zones.Zones.ContainsKey(id))
            {
                return results;
            }

            switch (resourceType)
            {
                case ZoneResourceType.Dungeons:
                    {
                        var addedCount = 0;

                        for (var i = 0; i < Dungeons.DungeonManager.Dungeons.Count; i++)
                        {
                            var resource = Dungeons.DungeonManager.Dungeons[i];

                            if (resource.ZoneID == 0)
                            {
                                resource.ZoneID = id;
                                Dungeons.DungeonManager.SaveDungeon(i);

                                results.Add(new ZoneResource()
                                {
                                    Num = i,
                                    Name = resource.Name,
                                    Type = resourceType
                                });

                                addedCount++;
                            }

                            if (addedCount == amount)
                            {
                                break;
                            }
                        }
                    }
                    break;
                case ZoneResourceType.Items:
                    {
                        var addedCount = 0;

                        for (var i = 0; i < Items.ItemManager.Items.MaxItems; i++)
                        {
                            var resource = Items.ItemManager.Items[i];

                            if (resource.ZoneID == 0)
                            {
                                resource.ZoneID = id;
                                Items.ItemManager.SaveItem(i);

                                results.Add(new ZoneResource()
                                {
                                    Num = i,
                                    Name = resource.Name,
                                    Type = resourceType
                                });

                                addedCount++;
                            }

                            if (addedCount == amount)
                            {
                                break;
                            }
                        }
                    }
                    break;
                case ZoneResourceType.NPCs:
                    {
                        var addedCount = 0;

                        for (var i = 1; i < Npcs.NpcManager.Npcs.MaxNpcs; i++)
                        {
                            var resource = Npcs.NpcManager.Npcs[i];

                            if (resource.ZoneID == 0)
                            {
                                resource.ZoneID = id;
                                Npcs.NpcManager.SaveNpc(i);

                                results.Add(new ZoneResource()
                                {
                                    Num = i,
                                    Name = resource.Name,
                                    Type = resourceType
                                });

                                addedCount++;
                            }

                            if (addedCount == amount)
                            {
                                break;
                            }
                        }
                    }
                    break;
                case ZoneResourceType.RDungeons:
                    {
                        var addedCount = 0;

                        for (var i = 0; i < RDungeons.RDungeonManager.RDungeons.Count; i++)
                        {
                            var resource = RDungeons.RDungeonManager.RDungeons[i];

                            if (resource.ZoneID == 0)
                            {
                                resource.ZoneID = id;
                                RDungeons.RDungeonManager.SaveRDungeon(i);

                                results.Add(new ZoneResource()
                                {
                                    Num = i,
                                    Name = resource.DungeonName,
                                    Type = resourceType
                                });

                                addedCount++;
                            }

                            if (addedCount == amount)
                            {
                                break;
                            }
                        }
                    }
                    break;
                case ZoneResourceType.Shops:
                    {
                        var addedCount = 0;

                        for (var i = 1; i < Shops.ShopManager.Shops.MaxShops; i++)
                        {
                            var resource = Shops.ShopManager.Shops[i];

                            if (resource.ZoneID == 0)
                            {
                                resource.ZoneID = id;
                                Shops.ShopManager.SaveShop(i);

                                results.Add(new ZoneResource()
                                {
                                    Num = i,
                                    Name = resource.Name,
                                    Type = resourceType
                                });

                                addedCount++;
                            }

                            if (addedCount == amount)
                            {
                                break;
                            }
                        }
                    }
                    break;
                case ZoneResourceType.Stories:
                    {
                        var addedCount = 0;

                        for (var i = 1; i < Stories.StoryManager.Stories.MaxStories; i++)
                        {
                            var resource = Stories.StoryManager.Stories[i];

                            if (resource.ZoneID == 0)
                            {
                                resource.ZoneID = id;
                                Stories.StoryManager.SaveStory(i);

                                results.Add(new ZoneResource()
                                {
                                    Num = i,
                                    Name = resource.Name,
                                    Type = resourceType
                                });

                                addedCount++;
                            }

                            if (addedCount == amount)
                            {
                                break;
                            }
                        }
                    }
                    break;
                case ZoneResourceType.Maps:
                    {
                        using (var dbConnection = new DatabaseConnection(DatabaseID.Data))
                        {
                            var pendingZonedMaps = Maps.MapManager.LoadPendingZonedMaps(dbConnection.Database, amount);

                            foreach (var pendingZonedMap in pendingZonedMaps)
                            {
                                var map = Maps.MapManager.RetrieveMap(pendingZonedMap);

                                map.ZoneID = id;

                                if (map.MapType != Enums.MapType.Standard)
                                {
                                    throw new NotSupportedException("Invalid map type.");
                                }

                                Maps.MapManager.SaveStandardMap(dbConnection, map.MapID, (Maps.Map)map);

                                results.Add(new ZoneResource()
                                {
                                    Num = map.MapID.Trim('s').ToInt(),
                                    Name = map.Name,
                                    Type = resourceType
                                });
                            }

                        }
                    }
                    break;
            }

            return results;
        }
    }
}
