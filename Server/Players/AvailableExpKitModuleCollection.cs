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

namespace Server.Players
{
    public class AvailableExpKitModuleCollection
    {
        List<AvailableExpKitModule> availableModules;

        public AvailableExpKitModule this[int index] {
            get { return availableModules[index]; }
        }

        public void Add(AvailableExpKitModule module) {
            availableModules.Add(module);
        }

        public void RemoveAt(int index) {
            availableModules.RemoveAt(index);
        }

        public void Remove(Enums.ExpKitModules type) {
            int index = IndexOf(type);
            if (index > -1) {
                RemoveAt(index);
            }
        }

        public int IndexOf(Enums.ExpKitModules type) {
            for (int i = 0; i < availableModules.Count; i++) {
                if (availableModules[i].Type == type) {
                    return i;
                }
            }
            return -1;
        }

        public bool Contains(Enums.ExpKitModules type) {
            return IndexOf(type) != -1;
        }

        public int Count {
            get { return availableModules.Count; }
        }

        public AvailableExpKitModuleCollection() {
            availableModules = new List<AvailableExpKitModule>();
        }

        public AvailableExpKitModuleCollection(int capacity) {
            availableModules = new List<AvailableExpKitModule>(capacity);
        }

    }
}
