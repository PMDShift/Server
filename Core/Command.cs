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

    public class Command
    {
        #region Fields

        List<string> commandArgs = new List<string>();
        string fullCommand;

        #endregion Fields

        #region Constructors

        internal Command(string fullCommand, List<string> command)
        {
            this.fullCommand = fullCommand;
            commandArgs = command;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the command line arguments for the program
        /// </summary>
        public List<string> CommandArgs
        {
            get { return commandArgs; }
        }

        /// <summary>
        /// Gets the full, unparsed command string
        /// </summary>
        public string FullCommand {
            get { return fullCommand; }
        }

        #endregion Properties

        #region Indexers

        public string this[int index]
        {
            get { return commandArgs[index]; }
        }

        #endregion Indexers

        #region Methods

        /// <summary>
        /// Checks if a certain argument is included in the command line
        /// </summary>
        /// <param name="argToFind">The argument to look for</param>
        /// <returns>True if the argument exists; False if it doesn't exist.</returns>
        public bool ContainsCommandArg(string argToFind)
        {
            return commandArgs.Contains(argToFind);
        }

        /// <summary>
        /// Retrives the index of a certain argument in the command line.
        /// </summary>
        /// <param name="argToFind"></param>
        /// <returns>The index of the argument if it was found; otherwise, returns -1</returns>
        public int FindCommandArg(string argToFind)
        {
            return commandArgs.IndexOf(argToFind);
        }

        #endregion Methods
    }
}