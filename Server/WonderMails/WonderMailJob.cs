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


namespace Server.WonderMails
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using DataManager.Players;

    using Server.Players;

    public class WonderMailJob
    {
        #region Fields

        WonderMail mission;

        #endregion Fields

        #region Constructors

        public WonderMailJob(PlayerDataJobListItem baseJobItem) {
            
            mission = new WonderMail(baseJobItem);
        }

        public WonderMailJob(WonderMail mail) {
            mission = mail;
        }

        public WonderMailJob(WonderMailJob mail) {
            mission = new WonderMail(new PlayerDataJobListItem());
            mission.Data1 = mail.Mission.Data1;
            mission.Data2 = mail.Mission.Data2;
            mission.Difficulty = mail.Mission.Difficulty;
            mission.DungeonIndex = mail.Mission.DungeonIndex;
            mission.DungeonMapNum = mail.Mission.DungeonMapNum;
            mission.GoalMapIndex = mail.Mission.GoalMapIndex;
            mission.GoalName = mail.Mission.GoalName;
            mission.LoseStoryScript = mail.Mission.LoseStoryScript;
            mission.MissionClientIndex = mail.Mission.MissionClientIndex;
            mission.MissionType = mail.Mission.MissionType;
            mission.Mugshot = mail.Mission.Mugshot;
            mission.RDungeon = mail.Mission.RDungeon;
            mission.RDungeonFloor = mail.Mission.RDungeonFloor;
            mission.RewardIndex = mail.Mission.RewardIndex;
            mission.StartStoryScript = mail.Mission.StartStoryScript;
            mission.Summary = mail.Mission.Summary;
            mission.TargetIndex = mail.Mission.TargetIndex;
            mission.Title = mail.Mission.Title;
            mission.WinStoryScript = mail.Mission.WinStoryScript;
        }

        #endregion Constructors

        #region Properties

        public Enums.JobStatus Accepted {
            get { return (Enums.JobStatus)mission.RawMission.Accepted; }
            set { mission.RawMission.Accepted = (int)value; }
        }

        public PlayerDataJobListItem RawJob {
            get { return mission.RawMission; }
        }

        public int SendsRemaining {
            get { return mission.RawMission.SendsRemaining; }
            set { mission.RawMission.SendsRemaining = value; }
        }

        public WonderMail Mission {
            get { return mission; }
        }

        #endregion Properties
    }
}