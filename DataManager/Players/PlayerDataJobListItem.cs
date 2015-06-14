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

namespace DataManager.Players
{
    public class PlayerDataJobListItem
    {
        public int Accepted { get; set; }
        public int SendsRemaining { get; set; }



        public int MissionClientIndex { get; set; }
        public int TargetIndex { get; set; }
        public int RewardIndex { get; set; }

        public int MissionType { get; set; }

        /// <summary>
        /// Item number if Mission Type is Item Retrieval. If Escort, pokemon number.
        /// </summary>
        public int Data1 { get; set; }

        /// <summary>
        /// Item amount if Mission Type is Item Retrieval.
        /// </summary>
        public int Data2 { get; set; }

        public int DungeonIndex { get; set; }
        //map index or RDungeon index
        public int GoalMapIndex { get; set; }
        //floor for an RDungeon, 0 otherwise
        public bool RDungeon { get; set; }

        public int StartStoryScript { get; set; }
        public int WinStoryScript { get; set; }
        public int LoseStoryScript { get; set; }
    }
}
