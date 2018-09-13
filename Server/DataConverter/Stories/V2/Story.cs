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

namespace Server.DataConverter.Stories.V2
{
    public class Story
    {
        #region Fields

        public List<int> ExitAndContinue;

        #endregion Fields

        #region Constructors

        public Story()
        {
            ExitAndContinue = new List<int>();
        }

        #endregion Constructors

        #region Properties

        public int MaxSegments
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public int Revision
        {
            get;
            set;
        }

        public StorySegment[] Segments
        {
            get;
            set;
        }

        public int StoryStart
        {
            get;
            set;
        }

        public int Version
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods

        public void UpdateStorySegments()
        {
            Segments = new StorySegment[MaxSegments + 1];
        }

        #endregion Methods
    }
}
