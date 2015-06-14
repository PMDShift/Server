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
using System.Net;
using System.Xml;

using Server.Players;


namespace Server.Statistics
{
    public class PlayerStatistics
    {
        Player owner;

        public TimeSpan TotalPlayTime {
            get { return owner.PlayerData.TotalPlayTime; }
        }

        public DateTime LoginTime {
            get { return owner.PlayerData.LastLogin; }
        }

        public PlayerStatistics(Player owner) {
            this.owner = owner;
        }

        public void HandleLogin(string os, string dotNetVersion, string macAddress, IPAddress ipAddress)
        {
            owner.PlayerData.LastLogin = DateTime.UtcNow;

            owner.PlayerData.LastOS = os;
            owner.PlayerData.LastDotNetVersion = dotNetVersion;
            owner.PlayerData.LastMacAddressUsed = macAddress;
            owner.PlayerData.LastIPAddressUsed = ipAddress.ToString();
        }

        public void HandleLogout() {
            owner.PlayerData.LastLogout = DateTime.UtcNow;
        }

        public void Save() {
            if (owner.PlayerData.TotalPlayTime == null) {
                owner.PlayerData.TotalPlayTime = new TimeSpan();
            }

            owner.PlayerData.TotalPlayTime += DetermineLastPlayTime();
        }

        public TimeSpan DetermineLastPlayTime() {
            if (owner.PlayerData.LastLogin != DateTime.MinValue && owner.PlayerData.LastLogout != DateTime.MinValue) {
                return owner.PlayerData.LastLogout - owner.PlayerData.LastLogin;
            } else {
                return TimeSpan.Zero;
            }
        }

    }
}
