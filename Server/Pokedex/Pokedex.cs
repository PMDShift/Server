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


namespace Server.Pokedex
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using PMDCP.DatabaseConnector.MySql;
    using Server.Database;


    public class Pokedex
    {
        #region Fields

        static Pokemon[] pokemon;
        static PMDCP.Core.ListPair<int, int> spriteReorderList;

        #endregion Fields

        #region Events

        public static event EventHandler LoadComplete;

        public static event EventHandler<LoadingUpdateEventArgs> LoadUpdate;

        #endregion Events

        #region Methods

        public static void CreateDataFiles() {
            //for (int i = 0; i < pokemon.Length; i++) {
            //    if (!IO.IO.FileExists(IO.Paths.PokedexFolder + i.ToString() + ".xml")) {
            //        SavePokemon(i);
            //    }
            //}
        }

        public static Pokemon GetPokemon(int id) {
            return pokemon[id];
        }

        public static PokemonForm GetPokemonForm(int id) {
            return GetPokemonForm(id, 0);
        }

        public static PokemonForm GetPokemonForm(int id, int form) {
            if (pokemon[id].Forms.Count > form) {
                return pokemon[id].Forms[form];
            } else {
                return pokemon[id].Forms[0];
            }
        }

        public static int GetUpdatedSprite(int oldSprite) {
            if (spriteReorderList != null) {
                // This means we found a sprite update...
                return spriteReorderList.GetKey(oldSprite);
            } else {
                return oldSprite;
            }
        }

        public static void Initialize() {
            pokemon = new Pokemon[Constants.TOTAL_POKEMON + 1];
            for (int i = 0; i < pokemon.Length; i++) {
                pokemon[i] = new Pokemon();
                pokemon[i].ID = i;
            }
            //CreateDataFiles();
        }

        public static void LoadAllPokemon() {

            using (DatabaseConnection dbConnection = new DatabaseConnection(DatabaseID.Data)) {
                for (int i = 0; i < pokemon.Length; i++) {
                    LoadPokemon(dbConnection, i);
                    //if (spriteReorderList != null) {
                    //    pokemon[i].Sprite = spriteReorderList.GetKey(pokemon[i].Sprite);
                    //    SavePokemon(i);
                    //}
                    if (LoadUpdate != null)
                        LoadUpdate(null, new LoadingUpdateEventArgs(i, Constants.TOTAL_POKEMON));
                }
                //if (spriteReorderList != null) {
                //System.IO.File.Delete(IO.Paths.PokedexFolder + "sprite-reorder-list.txt");
                //}
            }
            if (LoadComplete != null)
                LoadComplete(null, null);
        }

        public static void LoadPokemon(DatabaseConnection dbConnection, int id) {
            //if (IO.IO.FileExists(IO.Paths.PokedexFolder + id.ToString() + ".xml")) {
            pokemon[id].Load(dbConnection);
            //}
        }

        /*
        public static void SavePokemon(int id) {
            pokemon[id].Save();
        }
        */

        public static Pokemon FindByName(string name) {
            for (int i = 1; i < pokemon.Length; i++) {
                if (pokemon[i].Name.ToLower() == name.ToLower()) {
                    return pokemon[i];
                }
            }
            return null;
        }

        public static Pokemon FindBySprite(int sprite) {
            //for (int i = 1; i < pokemon.Length; i++) {
            //    foreach (PokemonForm form in pokemon[i].Forms) {
            //        for (int j = 0; j < 2; j++) {
            //            for (int k = 0; j < 3; k++) {
            //                if (form.Sprite[j, k] == sprite) {
            //                    return pokemon[i];
            //                }
            //            }
            //        }
            //    }
            //}
            //return null;
            if (sprite >= 1 && sprite < pokemon.Length)
            {
                return pokemon[sprite];
            }
            else
            {
                return null;
            }
        }

        //public static Pokemon FindByMugshot(int mugshot) {
        //    for (int i = 1; i < pokemon.Length; i++) {
        //        foreach (PokemonForm form in pokemon[i].Forms) {
        //            for (int j = 0; j < 2; j++) {
        //                for (int k = 0; j < 3; k++) {
        //                    if (form.Mugshot[j, k] == mugshot) {
        //                        return pokemon[i];
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return null;
        //}

        public static List<Pokemon> FindAllByName(string startOfName) {
            List<Pokemon> list = new List<Pokemon>();
            for (int i = 1; i < pokemon.Length; i++) {
                if (pokemon[i].Name.ToLower().StartsWith(startOfName.ToLower())) {
                    list.Add(pokemon[i]);
                }
            }
            return list;
        }

        #endregion Methods
    }
}