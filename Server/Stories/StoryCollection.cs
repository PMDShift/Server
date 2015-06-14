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
using PMDCP.Core;

namespace Server.Stories
{
    public class StoryCollection
    {
        #region Fields

        ListPair<int, Story> stories;
        int maxStories;

        #endregion Fields

        #region Constructors

        public StoryCollection(int maxStories) {
            if (maxStories == 0)
                maxStories = 50;
            this.maxStories = maxStories;
            stories = new ListPair<int, Story>();
        }

        #endregion Constructors

        #region Properties

        public ListPair<int, Story> Stories {
            get { return stories; }
        }

        public int MaxStories {
            get { return maxStories; }
        }

        #endregion Properties

        #region Indexers

        public Story this[int index] {
            get { return stories[index]; }
        }

        #endregion Indexers
    }
}
