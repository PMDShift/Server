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
using System.Linq;
using System.Text;
using PMDCP.Core;
using System.Xml;
using System.IO;

namespace Server.Players
{
    public class StoryHelper
    {
        Player owner;

        public StoryHelper(Player owner)
        {
            this.owner = owner;
        }

        private void VerifyDirectory(string directory)
        {
            if (Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        public void SaveSetting(string key, string value)
        {
            if (owner.PlayerData.StoryHelperStateSettings.ContainsKey(key) == false)
            {
                owner.PlayerData.StoryHelperStateSettings.Add(key, value);
            }
            else
            {
                owner.PlayerData.StoryHelperStateSettings.SetValue(key, value);
            }
        }

        public string ReadSetting(string key)
        {
            int index = owner.PlayerData.StoryHelperStateSettings.IndexOfKey(key);
            if (index > -1)
            {
                return owner.PlayerData.StoryHelperStateSettings.ValueByIndex(index);
            }
            else
            {
                return null;
            }
        }
    }
}
