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
using PMDCP.Core;

namespace Server.Dungeons
{
    public class Dungeon
    {
        private List<StandardDungeonMap> standardMaps;
        private List<RandomDungeonMap> randomMaps;

        public string Name { get; set; }
        public bool IsSandboxed { get; set; }
        public int ZoneID { get; set; }

        public List<StandardDungeonMap> StandardMaps
        {
            get { return standardMaps; }
        }
        public List<RandomDungeonMap> RandomMaps
        {
            get { return randomMaps; }
        }

        public bool AllowsRescue { get; set; }

        public ListPair<int, string> ScriptList { get; set; }

        public Dungeon()
        {
            standardMaps = new List<StandardDungeonMap>();
            randomMaps = new List<RandomDungeonMap>();
            Name = "";
            ScriptList = new ListPair<int, string>();
        }
    }
}
