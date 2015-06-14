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

namespace Server.IO
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class IO
    {
        #region Methods

        public static void Initialize(string startFolder)
        {
            Paths.Initialize(startFolder);
        }

        public static bool DirectoryExists(string dirPath) {
            return System.IO.Directory.Exists(ProcessPath(dirPath));
        }

        public static void CreateDirectory(string dirPath) {
            if (DirectoryExists(dirPath) == false) {
                System.IO.Directory.CreateDirectory(ProcessPath(dirPath));
            }
        }

        public static bool FileExists(string filePath) {
            return System.IO.File.Exists(ProcessPath(filePath));
        }

        public static string ProcessPath(string path) {
            return path;
        }

        #endregion Methods
    }
}