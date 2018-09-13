using System;
using System.Text;
using DataManager.Players;
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


#region Header

/*
 * Created by SharpDevelop.
 * User: Pikachu
 * Date: 15/10/2009
 * Time: 11:07 PM
 *
 */

#endregion Header

namespace Server.WonderMails
{
    /// <summary>
    /// Description of WonderMail.
    /// </summary>
    public class WonderMail
    {
        #region Fields

        PlayerDataJobListItem baseJobItem;

        #endregion Fields

        #region Constructors

        public WonderMail(PlayerDataJobListItem baseJobItem)
        {
            this.baseJobItem = baseJobItem;
        }

        #endregion Constructors

        #region Properties

        public PlayerDataJobListItem RawMission { get { return baseJobItem; } }

        public int MissionClientIndex
        {
            get { return baseJobItem.MissionClientIndex; }
            set { baseJobItem.MissionClientIndex = value; }
        }

        public int TargetIndex
        {
            get { return baseJobItem.TargetIndex; }
            set { baseJobItem.TargetIndex = value; }
        }

        public int RewardIndex
        {
            get { return baseJobItem.RewardIndex; }
            set { baseJobItem.RewardIndex = value; }
        }

        public Enums.MissionType MissionType
        {
            get { return (Enums.MissionType)baseJobItem.MissionType; }
            set { baseJobItem.MissionType = (int)value; }
        }

        /// <summary>
        /// Item number if Mission Type is Item Retrieval. If Escort, pokemon number.
        /// </summary>
        public int Data1
        {
            get { return baseJobItem.Data1; }
            set { baseJobItem.Data1 = value; }
        }

        /// <summary>
        /// Item amount if Mission Type is Item Retrieval.
        /// </summary>
        public int Data2
        {
            get { return baseJobItem.Data2; }
            set { baseJobItem.Data2 = value; }
        }

        public int DungeonIndex
        {
            get { return baseJobItem.DungeonIndex; }
            set { baseJobItem.DungeonIndex = value; }
        }

        //map number or RDungeon index
        public int GoalMapIndex
        {
            get { return baseJobItem.GoalMapIndex; }
            set { baseJobItem.GoalMapIndex = value; }
        }

        //floor for an RDungeon, 0 otherwise
        public bool RDungeon
        {
            get { return baseJobItem.RDungeon; }
            set { baseJobItem.RDungeon = value; }
        }

        public int StartStoryScript
        {
            get { return baseJobItem.StartStoryScript; }
            set { baseJobItem.StartStoryScript = value; }
        }

        public int WinStoryScript
        {
            get { return baseJobItem.WinStoryScript; }
            set { baseJobItem.WinStoryScript = value; }
        }

        public int LoseStoryScript
        {
            get { return baseJobItem.LoseStoryScript; }
            set { baseJobItem.LoseStoryScript = value; }
        }

        //extra, calculated info
        public int DungeonMapNum { get; set; }
        public int RDungeonFloor { get; set; }
        public Enums.JobDifficulty Difficulty { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string GoalName { get; set; }
        public int Mugshot { get; set; }

        #endregion Properties
    }
}