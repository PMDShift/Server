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

namespace Server
{
    public static class Extensions
    {
        public static int ToInt(this string str)
        {
            int result = 0;
            if (!string.IsNullOrEmpty(str) && int.TryParse(str, out result))
            {
                return result;
            }
            else
                return 0;
        }

        public static int ToInt(this string str, int defaultVal)
        {
            int result = 0;
            if (str != null && int.TryParse(str, out result) == true)
            {
                return result;
            }
            else
                return defaultVal;
        }

        public static double ToDbl(this string str)
        {
            double result = 0;
            if (str != null && double.TryParse(str, out result) == true)
            {
                return result;
            }
            else
                return 0;
        }

        public static double ToDbl(this string str, double defaultVal)
        {
            double result = 0;
            if (str != null && double.TryParse(str, out result) == true)
            {
                return result;
            }
            else
                return defaultVal;
        }

        public static string ToIntString(this bool boolval)
        {
            if (boolval == true)
                return "1";
            else
                return "0";
        }

        public static bool IsNumeric(this string str)
        {
            int result;
            return int.TryParse(str, out result);
        }

        public static ulong ToUlng(this string str)
        {
            ulong result = 0;
            if (ulong.TryParse(str, out result) == true)
            {
                return result;
            }
            else
                return 0;
        }

        public static bool ToBool(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                switch (str.ToLower())
                {
                    case "true":
                        return true;
                    case "false":
                        return false;
                    case "1":
                        return true;
                    case "0":
                        return false;
                    case "yes":
                        return true;
                    case "no":
                        return false;
                    default:
                        return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool ToBool(this object obj)
        {
            if (obj != null)
            {
                return (bool)obj;
            }
            else
            {
                return false;
            }
        }

        public static int ToInteger(this object obj)
        {
            if (obj != null)
            {
                return Convert.ToInt32(obj);
            }
            else
            {
                return 0;
            }
        }

        public static DateTime? ToDate(this string date)
        {
            DateTime tmpDate;
            if (DateTime.TryParse(date, out tmpDate))
            {
                return tmpDate;
            }
            else
            {
                return null;
            }
        }
    }
}

