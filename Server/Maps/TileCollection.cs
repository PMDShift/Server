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
using DataManager.Maps;

namespace Server.Maps
{
    public class TileCollection
    {
        RawMap rawMap;
        Tile[,] tiles;

        public TileCollection(RawMap rawMap) {
            this.rawMap = rawMap;
            tiles = new Tile[rawMap.MaxX + 1, rawMap.MaxY + 1];

            if (rawMap.Tile != null) {
                for (int x = 0; x <= MaxX; x++) {
                    for (int y = 0; y <= MaxY; y++) {
                        if (rawMap.Tile[x, y] == null) {
                            rawMap.Tile[x, y] = new DataManager.Maps.Tile();
                        }
                        tiles[x, y] = new Tile(rawMap.Tile[x, y]);
                    }
                }
            }
        }

        public TileCollection(RawMap rawMap, int maxX, int maxY) {
            this.rawMap = rawMap;
            rawMap.MaxX = maxX;
            rawMap.MaxY = maxY;

            rawMap.Tile = new DataManager.Maps.Tile[MaxX + 1, MaxY + 1];
            tiles = new Tile[MaxX + 1, MaxY + 1];

            for (int x = 0; x <= MaxX; x++) {
                for (int y = 0; y <= MaxY; y++) {
                    if (rawMap.Tile[x, y] == null) {
                        rawMap.Tile[x, y] = new DataManager.Maps.Tile();
                    }
                    tiles[x, y] = new Tile(rawMap.Tile[x, y]);
                }
            }
        }

        public int MaxX {
            get {
                return rawMap.MaxX;
            }
        }

        public int MaxY {
            get {
                return rawMap.MaxY;
            }
        }

        public Tile this[int x, int y] {
            get {
                return tiles[x, y];
            }
            set {
                tiles[x, y] = value;
                //rawMap.Tile[x, y] = value.RawTile;
            }
        }
    }
}
