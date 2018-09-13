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
using System.Text;

namespace Server.Maps
{
    public class RDungeonMap : BasicMap, IMap
    {
        internal DataManager.Maps.RDungeonMap baseMap;
        Object lockObject = new object();

        public const string ID_PREFIX = "rd";

        public RDungeonMap(DataManager.Maps.RDungeonMap baseMap)
            : base(baseMap)
        {
            this.baseMap = baseMap;
        }

        public Enums.MapType MapType
        {
            get { return Enums.MapType.RDungeonMap; }
        }

        public string IDPrefix
        {
            get { return ID_PREFIX; }
        }

        public bool Cacheable
        {
            get { return false; }
        }

        public int RDungeonIndex
        {
            get { return baseMap.RDungeonIndex; }
        }

        public int RDungeonFloor
        {
            get { return baseMap.RDungeonFloor; }
        }

        public int StartX
        {
            get { return baseMap.StartX; }
            set { baseMap.StartX = value; }
        }
        public int StartY
        {
            get { return baseMap.StartY; }
            set { baseMap.StartY = value; }
        }

        public bool IsProcessingComplete()
        {
            return true;
        }

        public void Save()
        {
            lock (lockObject)
            {
                using (Database.DatabaseConnection dbConnection = new Database.DatabaseConnection(Database.DatabaseID.Data))
                {
                    MapManager.SaveRDungeonMap(dbConnection, MapID, this);
                }
            }
        }
    }
}
