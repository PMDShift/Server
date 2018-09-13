using System;
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
 * Date: 17/10/2009
 * Time: 11:07 PM
 *
 */

#endregion Header

namespace Server.WonderMails
{
    /// <summary>
    /// Description of RewardItem.
    /// </summary>
    public class MissionRewardData
    {
        public int ItemNum
        {
            get;
            set;
        }

        public int Amount
        {
            get;
            set;
        }

        public string Tag
        {
            get;
            set;
        }

        public MissionRewardData()
        {
        }
    }
}