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
using PMDCP.DatabaseConnector.MySql;
using PMDCP.DatabaseConnector;
using Server.Database;
using System.Collections.Concurrent;

namespace Server.Statistics
{
    class PacketStatistics
    {
        static ConcurrentDictionary<string, PacketStatistic> statistics;

        public static void Initialize()
        {
            statistics = new ConcurrentDictionary<string, PacketStatistic>();
        }


        public static void AddStatistic(string header, long processTime, long dataSplittingTime, long cpuTime)
        {
            PacketStatistic stats = statistics.GetOrAdd(header, new PacketStatistic());

            stats.TimesReceived++;
            stats.TotalProcessTime += processTime;
            stats.TotalDataSplittingTime += dataSplittingTime;
            stats.CPUUsage += cpuTime;
        }

        public static void ClearStatistics()
        {
            statistics.Clear();
        }

        public static void DumpStatistics()
        {
            using (DatabaseConnection dbConnection = new DatabaseConnection(DatabaseID.Data))
            {
                dbConnection.Database.ExecuteNonQuery("TRUNCATE TABLE packet_statistics");

                foreach (string key in statistics.Keys)
                {
                    PacketStatistic stats;
                    if (statistics.TryGetValue(key, out stats))
                    {
                        dbConnection.Database.UpdateOrInsert("packet_statistics", new IDataColumn[] {
                        dbConnection.Database.CreateColumn(true, "Header", key),
                        dbConnection.Database.CreateColumn(false, "Amount", stats.TimesReceived.ToString()),
                        dbConnection.Database.CreateColumn(false, "AverageTime", (stats.TotalProcessTime / stats.TimesReceived * 1000 / (double)System.Diagnostics.Stopwatch.Frequency).ToString()),
                        dbConnection.Database.CreateColumn(false, "AverageDataSplittingTime", (stats.TotalDataSplittingTime / stats.TimesReceived * 1000 / (double)System.Diagnostics.Stopwatch.Frequency).ToString()),
                        dbConnection.Database.CreateColumn(false, "AverageCPUUsage", (stats.CPUUsage / stats.TimesReceived).ToString())
                    });
                    }
                }
            }
        }
    }
}
