using System;
using System.Collections.Generic;
using System.Text;

using Server.Items;
using Server.Npcs;
using Server.Players;
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
    public class InstancedMap : BasicMap, IMap
    {
        internal DataManager.Maps.InstancedMap baseMap;

        Object lockObject = new object();

        public TurnManager TurnManager => new TurnManager(this);

        #region Constructors

        public InstancedMap(DataManager.Maps.InstancedMap baseMap)
            : base(baseMap)
        {
            this.baseMap = baseMap;
        }

        public InstancedMap(string mapID)
            : this(new DataManager.Maps.InstancedMap(mapID, Constants.MAX_MAP_NPCS, Constants.MAX_MAP_ITEMS))
        {
        }

        #endregion Constructors

        public Enums.MapType MapType
        {
            get { return Enums.MapType.Instanced; }
        }

        public bool Cacheable
        {
            get { return false; }
        }

        public string IDPrefix
        {
            get { return "i"; }
        }

        public int MapBase
        {
            get { return baseMap.MapBase; }
            set { baseMap.MapBase = value; }
        }

        public bool IsProcessingComplete()
        {
            //for (int i = 0; i < Npc.Length; i++) {
            //    if (Npc[i].NpcNum > 0) {
            //        if (ActiveNpc[i].Num == 0) {
            //            // An npc is still dead, so processing of this map is incomplete
            //            return false;
            //        }
            //    }
            //}
            return true;
        }

        public void Save()
        {
            lock (lockObject)
            {
                using (Database.DatabaseConnection dbConnection = new Database.DatabaseConnection(Database.DatabaseID.Data))
                {
                    MapManager.SaveInstancedMap(dbConnection, MapID, this);
                }
            }
        }
    }
}
