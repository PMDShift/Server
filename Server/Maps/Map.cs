using System;
using System.Collections.Generic;
using System.Text;
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
    public class Map : BasicMap, IMap
    {
        internal DataManager.Maps.Map baseMap;
        Object lockObject = new object();

        public TurnManager TurnManager => new TurnManager(this);

        #region Constructors

        public Map(DataManager.Maps.Map baseMap)
            : base(baseMap)
        {
            this.baseMap = baseMap;
        }

        #endregion Constructors

        #region Properties

        public bool Cacheable
        {
            get { return true; }
        }

        public string IDPrefix
        {
            get { return "s"; }
        }

        public bool Instanced
        {
            get { return baseMap.Instanced; }
            set { baseMap.Instanced = value; }
        }

        public int MapNum
        {
            get { return MapID.Remove(0, IDPrefix.Length).ToInt(); }
        }

        public Enums.MapType MapType
        {
            get { return Enums.MapType.Standard; }
        }

        #endregion Properties

        #region Methods

        public bool IsProcessingComplete()
        {
            if (Npc.Count < 1) return true;

            int npcsActive = 0;

            for (int i = 0; i < Constants.MAX_MAP_NPCS; i++)
            {
                if (ActiveNpc[i].Num > 0)
                {
                    npcsActive++;
                    // An npc is still dead, so processing of this map is incomplete

                }
            }

            if (npcsActive >= MaxNpcs)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion Methods

        public void Save()
        {
            lock (lockObject)
            {
                using (Database.DatabaseConnection dbConnection = new Database.DatabaseConnection(Database.DatabaseID.Data))
                {
                    MapManager.SaveStandardMap(dbConnection, MapID, this);
                }
            }
        }
    }
}
