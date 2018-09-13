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
using System.Diagnostics;
using System.Text;
using Server.Network;

namespace Server
{
    public class Globals
    {
        private static string serverStatus;

        public static Enums.Weather ServerWeather { get; set; }
        //public static int WeatherIntensity { get; set; }

        public static Enums.Time ServerTime { get; set; }

        public static Command CommandLine { get; set; }

        public static bool ServerClosed { get; set; }
        public static bool GMOnly { get; set; }

        public static bool FoolsMode { get; set; }

        public static bool PacketCaching { get; set; }

        public static Stopwatch LiveTime { get; set; }

        public static string ServerStatus
        {
            get { return serverStatus; }
            set
            {
                serverStatus = value;
                PacketHitList hitList = null;
                PacketHitList.MethodStart(ref hitList);
                hitList.AddPacketToAll(PacketBuilder.CreateServerStatus());
                PacketHitList.MethodEnded(ref hitList);
            }
        }
    }
}
