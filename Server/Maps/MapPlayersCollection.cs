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


namespace Server.Maps
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using Server.Network;

    public class MapPlayersCollection
    {
        #region Fields

        List<MapPlayer> playersOnMap;
        ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();

        #endregion Fields

        #region Constructors

        public MapPlayersCollection() {
            playersOnMap = new List<MapPlayer>();
        }

        #endregion Constructors

        #region Properties

        public int Count {
            get {
                rwLock.EnterReadLock();
                try {
                    return playersOnMap.Count;
                } finally {
                    rwLock.ExitReadLock();
                }
            }
        }

        #endregion Properties

        #region Methods

        public void Add(string playerID) {
            rwLock.EnterUpgradeableReadLock();
            try {
                if (UnsafeIndexOf(playerID) == -1) {

                    rwLock.EnterWriteLock();
                    try {
                        playersOnMap.Add(new MapPlayer(playerID, ClientManager.FindClientFromCharID(playerID)));
                    } finally {
                        rwLock.ExitWriteLock();
                    }

                }
            } finally {
                rwLock.ExitUpgradeableReadLock();
            }
        }

        public void Clear() {
            rwLock.EnterWriteLock();
            try {
                playersOnMap.Clear();
            } finally {
                rwLock.ExitWriteLock();
            }
        }

        public MapPlayer GetItemByIndex(int index) {
            rwLock.EnterReadLock();
            try {
                return playersOnMap[index];
            } finally {
                rwLock.ExitReadLock();
            }
        }

        public IEnumerable<MapPlayer> GetPlayers() {
            MapPlayer[] playersOnMapCopy;

            rwLock.EnterReadLock();
            try {
                playersOnMapCopy = playersOnMap.ToArray();
            } finally {
                rwLock.ExitReadLock();
            }

            for (int i = 0; i < playersOnMapCopy.Length; i++) {
                yield return playersOnMapCopy[i];
            }
        }

        public MapPlayer[] ToArray() {
            MapPlayer[] playersOnMapCopy;

            rwLock.EnterReadLock();
            try {
                playersOnMapCopy = playersOnMap.ToArray();
            } finally {
                rwLock.ExitReadLock();
            }

            return playersOnMapCopy;
        }

        public void Remove(string playerID) {
            rwLock.EnterUpgradeableReadLock();
            try {
                int index = UnsafeIndexOf(playerID);
                if (index > -1) {

                    rwLock.EnterWriteLock();
                    try {
                        playersOnMap.RemoveAt(index);
                    } finally {
                        rwLock.ExitWriteLock();
                    }

                }
            } finally {
                rwLock.ExitUpgradeableReadLock();
            }
        }

        private int UnsafeIndexOf(string playerID) {
            for (int i = 0; i < playersOnMap.Count; i++) {
                if (playersOnMap[i].PlayerID == playerID) {
                    return i;
                }
            }
            return -1;
        }

        #endregion Methods
    }
}