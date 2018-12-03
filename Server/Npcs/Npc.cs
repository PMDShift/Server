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
using Server.Stories;

namespace Server.Npcs
{
    public class Npc
    {
        public string Name { get; set; }
        public string AttackSay { get; set; }
        public string AttackSay2 { get; set; }
        public string AttackSay3 { get; set; }

        public int Form { get; set; }

        public Enums.NpcBehavior Behavior { get; set; }
        public int ShinyChance { get; set; }

        public int Species { get; set; }
        public bool SpawnsAtDay { get; set; }
        public bool SpawnsAtNight { get; set; }
        public bool SpawnsAtDawn { get; set; }
        public bool SpawnsAtDusk { get; set; }
        public NpcDrop[] Drops { get; set; }

        public int RecruitRate { get; set; }

        public int[] Moves { get; set; }

        public string AIScript { get; set; }

        public bool IsSandboxed { get; set; }
        public int ZoneID { get; set; }

        public Enums.Direction SpawnDirection { get; set; }
        public Enums.Weather SpawnWeather { get; set; }

        public int Story { get; set; }

        public Npc()
        {
            Name = "";
            AttackSay = "";
            AttackSay2 = "";
            AttackSay3 = "";
            Drops = new NpcDrop[Constants.MAX_NPC_DROPS];
            for (int i = 0; i < Constants.MAX_NPC_DROPS; i++)
            {
                Drops[i] = new NpcDrop();
            }
            Moves = new int[4];

            SpawnWeather = Enums.Weather.Ambiguous;
        }

        public Story CreateAttackSayStory()
        {
            var story = new Stories.Story();
            var segment = Stories.StoryBuilder.BuildStory();

            AppendAttackSayStory(segment);

            segment.AppendToStory(story);

            return story;
        }

        public Story CreateInvalidClientStory()
        {
            var story = new Stories.Story();
            var segment = Stories.StoryBuilder.BuildStory();

            Stories.StoryBuilder.AppendSaySegment(segment, this.Name.Trim() + ": Try talking to me again with the Crow client.", this.Species, 0, 0);

            segment.AppendToStory(story);

            return story;
        }

        public void AppendAttackSayStory(StoryBuilderSegment segment)
        {
            Stories.StoryBuilder.AppendSaySegment(segment, this.Name.Trim() + ": " + this.AttackSay.Trim(), this.Species, 0, 0);

            if (!string.IsNullOrEmpty(this.AttackSay2))
            {
                Stories.StoryBuilder.AppendSaySegment(segment, this.Name.Trim() + ": " + this.AttackSay2.Trim(), this.Species, 0, 0);
            }

            if (!string.IsNullOrEmpty(this.AttackSay3))
            {
                Stories.StoryBuilder.AppendSaySegment(segment, this.Name.Trim() + ": " + this.AttackSay3.Trim(), this.Species, 0, 0);
            }
        }
    }
}
