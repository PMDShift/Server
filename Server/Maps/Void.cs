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
using Server.Players;
using Server.Network;

namespace Server.Maps
{
    public class Void : BasicMap, IMap
    {
        public Enums.MapType MapType
        {
            get { return Enums.MapType.Void; }
        }

        public TurnManager TurnManager => new TurnManager(this);

        public int Down
        {
            get { return 0; }
            set { }
        }

        public int Left
        {
            get { return 0; }
            set { }
        }

        public string MapID
        {
            get { return "void-" + owner.CharID; }
            set { }
        }

        public string IDPrefix
        {
            get { return "void"; }
        }

        public Enums.MapMoral Moral
        {
            get { return Enums.MapMoral.None; }
            set { }
        }

        public string Music
        {
            get;
            set;
        }

        public string Name
        {
            get { return ""; }
            set { }
        }

        public string Owner
        {
            get { return ""; }
            set { }
        }

        public int Revision
        {
            get { return 1; }
            set { }
        }

        public int Right
        {
            get { return 0; }
            set { }
        }

        public int Up
        {
            get { return 0; }
            set { }
        }

        public bool HungerEnabled
        {
            get { return false; }
            set { }
        }

        public void Save()
        {
            // No saving The Void!
        }

        public void Save(string filePath)
        {
            // No saving The Void!
        }

        public bool Load()
        {
            return true;
        }

        public bool Load(string filePath)
        {
            return true;
        }

        public void RemakePlayersList()
        {
            PlayersOnMap.Clear();
            PlayersOnMap.Add(owner.CharID);
        }

        public Player PlayerOwner
        {
            get { return owner; }
        }

        public bool Cacheable
        {
            get { return false; }
        }

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

        public bool SafeExit { get; set; }
        Player owner;
        public Void(Player owner)
            : base(new DataManager.Maps.MapDump("void-" + owner.CharID, Constants.MAX_MAP_NPCS, Constants.MAX_MAP_ITEMS))
        {
            this.owner = owner;
            RemakePlayersList();
            MaxX = 19;
            MaxY = 14;
            Tile = new TileCollection(BaseMap, MaxX, MaxY);
            Load();
        }
    }
}
