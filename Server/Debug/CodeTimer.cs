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
using System.Diagnostics;

namespace Server.Debug
{
    public class CodeTimer
    {
        Stopwatch stopwatch;
        Dictionary<string, TimeSpan> resultsCollection;
        string currentSectionName;
        string codeSection;

        public CodeTimer(string codeSection)
        {
            stopwatch = new Stopwatch();
            resultsCollection = new Dictionary<string, TimeSpan>();
            this.codeSection = codeSection;
        }

        public void StartTimingSection(string sectionName)
        {
            currentSectionName = sectionName;
            stopwatch.Start();
        }

        public void EndTimingSection()
        {
            stopwatch.Stop();
            resultsCollection.Add(currentSectionName, stopwatch.Elapsed);
            stopwatch.Reset();
        }

        public string GetResults()
        {
            long totalTicks = 0;
            foreach (TimeSpan timeSpan in resultsCollection.Values)
            {
                totalTicks += timeSpan.Ticks;
            }
            StringBuilder resultString = new StringBuilder();
            resultString.Append("--- ");
            resultString.Append(codeSection);
            resultString.AppendLine(" ---");
            foreach (string sectionName in resultsCollection.Keys)
            {
                // Append section name
                resultString.Append("[");
                resultString.Append(sectionName);
                resultString.Append("] ");
                TimeSpan timeSpan = resultsCollection[sectionName];
                // Append time taken
                resultString.Append(timeSpan.ToString());
                // Append percentage of total time
                resultString.Append(" (");
                resultString.Append(PMDCP.Core.MathFunctions.CalculatePercent(timeSpan.Ticks, totalTicks));
                resultString.AppendLine("%)");
            }
            return resultString.ToString();
        }
    }
}
