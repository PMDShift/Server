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
using Server.Maps;
using System.Threading;

namespace Server.AI
{
    public class MapGC
    {
        internal System.Collections.Concurrent.ConcurrentQueue<string> cleanupQueue;
        ManualResetEvent resetEvent;

        public void SetWaitEvent()
        {
            resetEvent.Set();
        }

        public MapGC()
        {
            cleanupQueue = new System.Collections.Concurrent.ConcurrentQueue<string>();
            resetEvent = new ManualResetEvent(false);
        }

        public void AddMaps(List<string> mapList)
        {
            foreach (string map in mapList)
            {
                cleanupQueue.Enqueue(map);
            }
        }

        public void Cleanup()
        {
            while (true)
            { // Keep this thread alive!
                resetEvent.WaitOne(); // Wait until we should do another GC
                resetEvent.Reset();

                // Go through all queued items
                while (true)
                {
                    string mapID = null;
                    bool hasItem = cleanupQueue.TryDequeue(out mapID);
                    if (!hasItem)
                    {
                        break; // Break out of the loop
                    }

                    using (Database.DatabaseConnection dbConnection = new Database.DatabaseConnection(Database.DatabaseID.Data))
                    {
                        if (MapManager.GetTotalPlayersOnMap(dbConnection, mapID) == 0)
                        {
                            DataManager.Maps.MapDataManager.DeleteMap(dbConnection.Database, mapID);
                        }
                    }
                }
            }
            /*
             * old code for reference:
             * //lock (MapManager.ActiveMapLockObject) {
                bool shouldRemove = false;
                if (map.PlayersOnMap.Count == 0 && tickCount.Elapsed(map.ActivationTime, AIProcessor.MapTTL)) {
                    if (map.IsProcessingComplete()) {
                        shouldRemove = true;

                        for (int i = 1; i < 9; i++) {

                            IMap borderingMap = MapManager.RetrieveActiveBorderingMap(map, (Enums.MapID)i);
                            if (borderingMap != null) {
                                if (borderingMap.PlayersOnMap.Count > 0) {
                                    shouldRemove = false;
                                }
                            }
                        }

                        if (shouldRemove) {
                            MapManager.rwLock.EnterWriteLock();
                            try {
                                if (map.MapType == Enums.MapType.House) {
                                    // Only save the map here when logging out - otherwise, the map is saved when the player is saved
                                    map.Save();
                                }
                                if ((map.MapType == Enums.MapType.RDungeonMap || map.MapType == Enums.MapType.Instanced)) {
                                    using (Database.DatabaseConnection dbConnection = new Database.DatabaseConnection(Database.DatabaseID.Data)) {
                                        if (MapManager.GetTotalPlayersOnMap(dbConnection, map.MapID) == 0) {
                                            DataManager.Maps.MapDataManager.DeleteMap(dbConnection.Database, map.MapID);
                                        }
                                    }
                                }

                                MapManager.UnsafeRemoveActiveMap(map.MapID);
                            } finally {
                                MapManager.rwLock.ExitWriteLock();
                            }
                        }
                    }
                }
                //}
             */
        }
    }
}
