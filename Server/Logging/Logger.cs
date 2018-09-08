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

using System.IO;
using System.Collections.Concurrent;

namespace Server.Logging
{
    public class Logger
    {
        const int LOG_THRESHOLD = 1000;
        
        private static int logAmount;

        private static ConcurrentDictionary<string, StringBuilder> logs;

        public static void Initialize() {
            logs = new ConcurrentDictionary<string, StringBuilder>();
        }

        public static void AppendToLog(string logFilePath, string dataToAppend) {
            AppendToLog(logFilePath, dataToAppend, true);
        }

        public static void AppendToLog(string logFilePath, string dataToAppend, bool includeDate) {
            StringBuilder log = logs.GetOrAdd(logFilePath, new StringBuilder());


            if (includeDate) {
                log.AppendLine("[" + DateTime.Now.ToString() + "] " + dataToAppend);
            } else {
                log.AppendLine(dataToAppend);
            }

            logAmount++;

            if (logAmount > LOG_THRESHOLD) {
                SaveLogs();
                logAmount = 0;
            }

            //if (logFilePath.StartsWith("/")) {
            //    logFilePath = IO.Paths.LogsFolder + logFilePath.Substring(1, logFilePath.Length - 1);
            //}
            //if (System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(logFilePath)) == false) {
            //    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(logFilePath));
            //}
            //using (StreamWriter writer = new StreamWriter(logFilePath, true)) {
            //    if (includeDate) {
            //        dataToAppend = "[" + DateTime.Now.ToString() + "] " + dataToAppend;
            //    }
            //    writer.WriteLine(dataToAppend);
            //}
        }

        public static void SaveLogs() {
            foreach (string key in logs.Keys) {
                StringBuilder log;
                if (logs.TryGetValue(key, out log)) {
                    string logFilePath = key;
                    if (logFilePath.StartsWith("/")) {
                        logFilePath = Path.Combine(IO.Paths.LogsFolder, logFilePath.Substring(1, logFilePath.Length - 1));
                    }
                    if (System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(logFilePath)) == false) {
                        System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(logFilePath));
                    }
                    using (StreamWriter writer = new StreamWriter(logFilePath, true)) {
                        writer.Write(log);
                    }
                }

            }
            logs.Clear();
        }
    }
}
