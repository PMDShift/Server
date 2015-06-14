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


namespace Server
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class LoadingUpdateEventArgs : EventArgs
    {
        #region Fields

        int currentValue;
        int maxValue;
        int percent;

        #endregion Fields

        #region Constructors

        public LoadingUpdateEventArgs(int currentValue, int maxValue)
        {
            this.currentValue = currentValue;
            this.maxValue = maxValue;
            this.percent = Math.CalculatePercent(currentValue, maxValue);
        }

        #endregion Constructors

        #region Properties

        public int CurrentValue
        {
            get { return currentValue; }
        }

        public int MaxValue
        {
            get { return maxValue; }
        }

        public int Percent
        {
            get { return percent; }
        }

        #endregion Properties
    }
}