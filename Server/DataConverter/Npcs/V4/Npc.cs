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

namespace Server.DataConverter.Npcs.V4
{
    public class Npc
    {
        public string Name { get; set; }
        public string AttackSay { get; set; }

        public int Sprite { get; set; }
        public int SpawnSecs { get; set; }

        public Enums.NpcBehavior Behavior { get; set; }
        public int Range { get; set; }

        public int Species { get; set; }
        public bool Big { get; set; }
        public int SpawnTime { get; set; }
        public NpcDrop[] Drops { get; set; }

        public int RecruitRate { get; set; }
        public int RecruitLevel { get; set; }

        public int Spell { get; set; }
        public int Frequency { get; set; }

        public string AIScript { get; set; }

        public Npc() {
            Name = "";
            AttackSay = "";
            Drops = new NpcDrop[Constants.MAX_NPC_DROPS];
            for (int i = 0; i < Constants.MAX_NPC_DROPS; i++) {
                Drops[i] = new NpcDrop();
            }
        }
    }
}
