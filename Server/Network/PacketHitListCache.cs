// This file is part of Mystery Dungeon eXtended.

// Copyright (C) 2015 Pikablu, MDX Contributors, PMU Staff

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published
// by the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU Affero General Public License for more details.

// You should have received a copy of the GNU Affero General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PMDCP.Core;
using PMDCP.Sockets;
using Server.Maps;
using Server.Players.Parties;

namespace Server.Network
{
    public class PacketHitListCache
    {
        List<object[]> hitListCache;
        PacketHitList hitList;

        public int Count
        {
            get { return hitListCache.Count; }
        }

        public PacketHitListCache()
        {
            hitListCache = new List<Object[]>();
        }

        public void AddCacheItem(object[] itemInformation)
        {
            hitListCache.Add(itemInformation);
        }

        private IEnumerable<Client> GetMapClients(ListPair<IMap, List<Client>> clientCollection, IMap map)
        {
            int mapIndex = clientCollection.IndexOfKey(map);
            if (mapIndex > -1)
            {
                List<Client> clients = clientCollection.ValueByIndex(mapIndex);
                foreach (Client client in clients)
                {
                    yield return client;
                }
            }
            else
            {
                List<Client> clients = new List<Client>();
                foreach (Client client in map.GetClients())
                {
                    clients.Add(client);
                    yield return client;
                }
                clientCollection.Add(map, clients);
            }
        }

        private IEnumerable<IMap> GetBorderingMaps(ListPair<IMap, Object[]> borderingMapCollection, IMap centralMap)
        {
            int mapIndex = borderingMapCollection.IndexOfKey(centralMap);
            if (mapIndex > -1)
            {
                Object[] maps = borderingMapCollection.ValueByIndex(mapIndex);
                foreach (IMap map in maps)
                {
                    if (map != null)
                    {
                        yield return map;
                    }
                }
            }
            else
            {
                Object[] maps = new object[9];
                for (int borderingMapID = 0; borderingMapID < 9; borderingMapID++)
                {
                    if (MapManager.IsBorderingMapLoaded(centralMap, (Enums.MapID)borderingMapID))
                    {
                        IMap borderingMap = MapManager.RetrieveBorderingMap(centralMap, (Enums.MapID)borderingMapID, true);
                        if (borderingMap != null)
                        {
                            maps[borderingMapID] = borderingMap;
                            yield return borderingMap;
                        }
                    }
                }
                borderingMapCollection.Add(centralMap, maps);
            }
        }

        public void BuildHitList(PacketHitList hitList)
        {
            this.hitList = hitList;

            ListPair<IMap, List<Client>> clientCollection = new ListPair<IMap, List<Client>>();
            ListPair<IMap, Object[]> borderingMapCollection = new ListPair<IMap, object[]>();

            for (int i = 0; i < hitListCache.Count; i++)
            {
                object[] param = hitListCache[i];
                switch ((PacketHitListTargets)param[0])
                {
                    case PacketHitListTargets.Client:
                        {
                            AddPacket((Client)param[1], (TcpPacket)param[2]);
                        }
                        break;

                    case PacketHitListTargets.All:
                        {
                            foreach (Client client in ClientManager.GetClients())
                            {
                                if (client.IsPlaying())
                                {
                                    AddPacket(client, (TcpPacket)param[1]);
                                }
                            }
                        }
                        break;

                    case PacketHitListTargets.RangedMapBut:
                        {
                            Maps.IMap map = param[2] as IMap;
                            int range = (int)param[6];
                            Client client = param[1] as Client;
                            foreach (Client mapClient in GetMapClients(clientCollection, map))
                            {
                                if (mapClient != null && (client == null || mapClient.TcpID != client.TcpID))
                                {
                                    if (range == -1 || PacketHitList.IsInRange(range, (int)param[4], (int)param[5], mapClient.Player.X, mapClient.Player.Y))
                                    {
                                        AddPacket(mapClient, (TcpPacket)param[3]);
                                    }
                                }
                            }
                        }
                        break;
                    // TODO: Should be fixed... (PacketHitListCache)
                    //case PacketHitListTargets.PlayersInSightBut: {
                    //        Client sender = param[1] as Client;
                    //        IMap centralMap = param[2] as IMap;
                    //        int range = (int)param[6];
                    //        foreach (Client mapClient in centralMap.GetSurroundingClients(centralMap)) {
                    //            if (sender == null || sender != mapClient) {
                    //                if (range == -1 || SeamlessWorldHelper.IsInSight(centralMap, (int)param[4], (int)param[5], mapClient.Player.Map, mapClient.Player.X, mapClient.Player.Y)) {
                    //                    AddPacket(mapClient, (TcpPacket)param[3]);
                    //                }
                    //            }
                    //        }
                    //    }
                    //    break;

                    case PacketHitListTargets.Party:
                        {
                            Party party = param[1] as Party;
                            foreach (Client partyClient in party.GetOnlineMemberClients())
                            {
                                if (partyClient.IsPlaying())
                                {
                                    AddPacket(partyClient, (TcpPacket)param[2]);
                                }
                            }
                        }
                        break;

                        // TODO: Should be fixed... (PacketHitListCache)
                        //case PacketHitListTargets.Others: {
                        //        Combat.ICharacter source = param[1] as Combat.ICharacter;
                        //        IMap map = param[2] as IMap;
                        //        TcpPacket packet = param[3] as TcpPacket;
                        //        Enums.OutdateType updateType = (Enums.OutdateType)param[4];
                        //        foreach (IMap borderingMap in GetBorderingMaps(borderingMapCollection, map)) {
                        //            foreach (Client borderingMapClient in GetMapClients(clientCollection, borderingMap)) {
                        //                if (borderingMapClient.Player.GetActiveRecruit() != source) {
                        //                    if (AI.MovementProcessor.WillCharacterSeeCharacter(borderingMap, borderingMapClient.Player.GetActiveRecruit(), map, source)) {
                        //                        if (!borderingMapClient.Player.IsSeenCharacterSeen(source)) {
                        //                            borderingMapClient.Player.AddActivation(hitList, source);
                        //                        }
                        //                        this.AddPacket(borderingMapClient, packet);
                        //                    } else {
                        //                        if (updateType == Enums.OutdateType.Location && borderingMapClient.Player.IsSeenCharacterSeen(source)) {
                        //                            this.AddPacket(borderingMapClient, packet);
                        //                            //unsee character
                        //                            borderingMapClient.Player.RemoveUnseenCharacters();
                        //                        } else {
                        //                            borderingMapClient.Player.OutdateCharacter(source, updateType);
                        //                            //outdate character
                        //                            //}
                        //                            //} else {
                        //                            //outdate character
                        //                        }
                        //                    }
                        //                }
                        //            }
                        //        }
                        //    }
                        //    break;
                }
            }

            hitListCache.Clear();
        }

        private void AddPacket(Client client, TcpPacket packet)
        {
            hitList.AddPacket(client, packet, true);
        }
    }
}
