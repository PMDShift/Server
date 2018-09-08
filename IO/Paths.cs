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
    using System.IO;
    using System.Text;

    public class Paths
    {
        #region Fields

        static string dataFolder;
        static string itemsFolder;
        static string movesFolder;
        static string npcsFolder;
        static string rdungeonsFolder;
        static string startFolder;
        static string storiesFolder;
        static string shopsFolder;
        static string dungeonsFolder;
        static string logsFolder;
        static string scriptsFolder;

        #endregion Fields

        #region Properties

        public static string DataFolder {
            get { return dataFolder; }
        }

        public static string ItemsFolder {
            get { return itemsFolder; }
        }

        public static string MovesFolder {
            get { return movesFolder; }
        }

        public static string NpcsFolder {
            get { return npcsFolder; }
        }

        public static string RDungeonsFolder {
            get { return rdungeonsFolder; }
        }

        public static string StartFolder {
            get { return startFolder; }
        }

        public static string StoriesFolder {
            get { return storiesFolder; }
        }

        public static string ShopsFolder {
            get { return shopsFolder; }
        }

        public static string DungeonsFolder {
            get { return dungeonsFolder; }
        }

        public static string LogsFolder {
            get { return logsFolder; }
        }

        public static string ScriptsFolder {
            get { return scriptsFolder; }
        }

        public static string ScriptsIOFolder {
            get { return scriptsIOFolder; }
        }

        #endregion Properties

        #region Methods

        static string scriptsIOFolder;

        internal static void Initialize(string startFolder) {
            startFolder = System.IO.Path.GetFullPath(startFolder);
            Paths.startFolder = startFolder;
            Paths.dataFolder = Path.Combine(startFolder, "Data");
            Paths.itemsFolder = Path.Combine(startFolder, "Items");
            Paths.npcsFolder = Path.Combine(startFolder, "Npcs");
            Paths.movesFolder = Path.Combine(startFolder, "Moves");
            Paths.storiesFolder = Path.Combine(startFolder, "Stories");
            Paths.rdungeonsFolder = Path.Combine(startFolder, "RDungeons");
            Paths.shopsFolder = Path.Combine(startFolder, "Shops");
            Paths.dungeonsFolder = Path.Combine(startFolder, "Dungeons");
            Paths.logsFolder = Path.Combine(startFolder, "Logs");
            Paths.scriptsFolder = Path.Combine(startFolder, "Scripts");
            Paths.scriptsIOFolder = Path.Combine(startFolder, "Scripts", "ScriptIO");

            if (!IO.DirectoryExists(Paths.startFolder))
                IO.CreateDirectory(Paths.startFolder);
            if (!IO.DirectoryExists(Paths.dataFolder))
                IO.CreateDirectory(Paths.dataFolder);
            if (!IO.DirectoryExists(Paths.logsFolder))
                IO.CreateDirectory(Paths.logsFolder);
            if (!IO.DirectoryExists(Paths.scriptsFolder))
                IO.CreateDirectory(Paths.scriptsFolder);
            if (!IO.DirectoryExists(Paths.scriptsIOFolder))
                IO.CreateDirectory(Paths.scriptsIOFolder);

            if (!IO.DirectoryExists(Paths.itemsFolder))
                IO.CreateDirectory(Paths.itemsFolder);
            if (!IO.DirectoryExists(Paths.npcsFolder))
                IO.CreateDirectory(Paths.npcsFolder);
            if (!IO.DirectoryExists(Paths.movesFolder))
                IO.CreateDirectory(Paths.movesFolder);
            if (!IO.DirectoryExists(Paths.storiesFolder))
                IO.CreateDirectory(Paths.storiesFolder);
            if (!IO.DirectoryExists(Paths.rdungeonsFolder))
                IO.CreateDirectory(Paths.rdungeonsFolder);
            if (!IO.DirectoryExists(Paths.shopsFolder))
                IO.CreateDirectory(Paths.shopsFolder);
            if (!IO.DirectoryExists(Paths.dungeonsFolder))
                IO.CreateDirectory(Paths.dungeonsFolder);
        }

        #endregion Methods
    }
}