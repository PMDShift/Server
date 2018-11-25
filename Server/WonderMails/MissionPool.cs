﻿// This file is part of Mystery Dungeon eXtended.

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

namespace Server.WonderMails
{
    public class MissionPool
    {
        public bool IsSandboxed { get; set; }

        private List<MissionClientData> missionClients;
        private List<MissionEnemyData> enemies;
        private List<MissionRewardData> rewards;

        public List<MissionClientData> MissionClients
        {
            get { return missionClients; }
        }

        public List<MissionEnemyData> Enemies
        {
            get { return enemies; }
        }

        public List<MissionRewardData> Rewards
        {
            get { return rewards; }
        }

        public MissionPool()
        {
            missionClients = new List<MissionClientData>();
            enemies = new List<MissionEnemyData>();
            rewards = new List<MissionRewardData>();
        }
    }
}
