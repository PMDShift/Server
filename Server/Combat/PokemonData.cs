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

namespace Server.Combat
{
    public class PokemonData
    {
        public PokemonData()
        {
        }

        public PokemonData(PokemonData data)
        {
            DexNum = data.DexNum;
            Form = data.Form;
            Shiny = data.Shiny;
            Sex = data.Sex;
        }

        public static bool operator ==(PokemonData data1, PokemonData data2)
        {
            return (data1.DexNum == data2.DexNum &&
            data1.Form == data2.Form &&
            data1.Shiny == data2.Shiny &&
                data1.Sex == data2.Sex);
        }

        public static bool operator !=(PokemonData data1, PokemonData data2)
        {
            return !(data1 == data2);
        }

        public int DexNum { get; set; }
        public int Form { get; set; }
        public Enums.Coloration Shiny { get; set; }
        public Enums.Sex Sex { get; set; }
    }
}
