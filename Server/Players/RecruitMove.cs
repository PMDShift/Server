using System;
using System.Collections.Generic;
using System.Text;
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


namespace Server.Players
{
    public class RecruitMove
    {
        DataManager.Characters.Move rawMove;

        #region Constructors

        public RecruitMove(DataManager.Characters.Move rawMove)
        {
            this.rawMove = rawMove;
        }

        public RecruitMove()
        {
            rawMove = new DataManager.Characters.Move();

            MoveNum = -1;
            CurrentPP = -1;
            MaxPP = -1;
        }

        #endregion Constructors

        #region Properties

        public int CurrentPP
        {
            get { return rawMove.CurrentPP; }
            set { rawMove.CurrentPP = value; }
        }

        public int MaxPP
        {
            get { return rawMove.MaxPP; }
            set { rawMove.MaxPP = value; }
        }

        public int MoveNum
        {
            get { return rawMove.MoveNum; }
            set { rawMove.MoveNum = value; }
        }

        public bool Sealed
        {
            //get { return rawMove.Sealed; }
            //set { rawMove.Sealed = value; }
            get;
            set;
        }

        public int AttackTime
        {
            get;
            set;
        }

        #endregion Properties
    }
}