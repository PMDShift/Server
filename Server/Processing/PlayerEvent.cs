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
using PMDCP.Sockets;

namespace Server.Processing
{
    public class PlayerEvent
    {
        string data;
        string[] parameters;

        public string Data {
            get { return data; }
        }

        public string[] Parameters {
            get { return parameters; }
        }

        public PlayerEvent(string data) {
            this.data = data;
            this.parameters = data.Split(TcpPacket.SEP_CHAR);
        }

        public string this[int index] {
            get { return parameters[index]; }
        }
    }
}
