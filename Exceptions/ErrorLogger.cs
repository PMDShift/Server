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
using System.IO;
using System.Xml;

namespace Server.Exceptions
{
    public class ErrorLogger
    {
        public static void WriteToErrorLog(Exception exception, string optionalInfo) {
            try {
                string date = DateTime.Now.ToShortTimeString();
                string filePath = Path.Combine(IO.Paths.LogsFolder, "ErrorLog-" + DateTime.Now.ToShortDateString().Replace("/", "-") + ".txt");
                using (StreamWriter writer = new StreamWriter(filePath, true)) {
                    writer.WriteLine("--- " + DateTime.Now.ToLongTimeString() + " ---");
                    writer.WriteLine("Exception: " + exception.ToString());
                    if (!string.IsNullOrEmpty(optionalInfo)) {
                        writer.WriteLine("Additional Data: " + optionalInfo);
                    }
                    writer.WriteLine();
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void WriteToErrorLog(Exception exception) {
            WriteToErrorLog(exception, "");
        }

    }
}
