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
using Server.Network;
using System.Drawing;

namespace Server.Players
{
    public class Ranks
    {
        public static bool IsAllowed(Client client, Enums.Rank RankToCheck) {
            Enums.Rank PlayerRank = client.Player.Access;
            switch (RankToCheck) {
                case Enums.Rank.Normal:
                    return true;
                case Enums.Rank.Monitor:
                    if (PlayerRank == Enums.Rank.Monitor | PlayerRank == Enums.Rank.Mapper | PlayerRank == Enums.Rank.Developer | PlayerRank == Enums.Rank.Admin | PlayerRank == Enums.Rank.ServerHost | PlayerRank == Enums.Rank.Scripter) {
                        return true;
                    }
                    break;
                case Enums.Rank.Mapper:
                    if (PlayerRank == Enums.Rank.Mapper | PlayerRank == Enums.Rank.Developer | PlayerRank == Enums.Rank.Admin | PlayerRank == Enums.Rank.ServerHost | PlayerRank == Enums.Rank.Scripter) {
                        return true;
                    }
                    break;
                case Enums.Rank.Developer:
                    if (PlayerRank == Enums.Rank.Developer | PlayerRank == Enums.Rank.Admin | PlayerRank == Enums.Rank.ServerHost | PlayerRank == Enums.Rank.Scripter) {
                        return true;
                    }
                    break;
                case Enums.Rank.Admin:
                    if (PlayerRank == Enums.Rank.Admin | PlayerRank == Enums.Rank.ServerHost | PlayerRank == Enums.Rank.Scripter) {
                        return true;
                    }
                    break;
                case Enums.Rank.ServerHost:
                    if (PlayerRank == Enums.Rank.ServerHost | PlayerRank == Enums.Rank.Scripter) {
                        return true;
                    }
                    break;
                case Enums.Rank.Scripter:
                    if (PlayerRank == Enums.Rank.Scripter) {
                        return true;
                    }
                    break;
            }
            return false;
        }

        public static bool IsDisallowed(Client client, Enums.Rank RankToCheck) {
            return !IsAllowed(client, RankToCheck);
        }

        public static Color GetRankColor(Enums.Rank rank) {
            switch (rank) {
                case Enums.Rank.Normal:
                    return Text.Trap;
                case Enums.Rank.Monitor:
                    return Text.Location;
                case Enums.Rank.Mapper:
                    return Text.NPC;
                case Enums.Rank.Developer:
                    return Text.Player;
                case Enums.Rank.Admin:
                    return Text.Item;
                case Enums.Rank.ServerHost:
                    return Text.Teammate;
                case Enums.Rank.Scripter:
                    return Text.MoveEffect;
                default:
                    return Color.DarkRed;
            }
        }
    }
}
