using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using PMDCP.DatabaseConnector.MySql;
using Server.Database;
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
    public class Pokemon
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public Enums.GrowthGroup GrowthGroup { get; set; }

        public string EggGroup1 { get; set; }

        public string EggGroup2 { get; set; }

        public string SpeciesName { get; set; }

        public List<PokemonForm> Forms { get; set; }

        public Pokemon()
        {
            Forms = new List<PokemonForm>();
        }

        /*
        public void Load() {
            int formNum = 0;
            while (IO.IO.FileExists(IO.Paths.PokedexFolder + ID + "-" + formNum + ".xml")) {
                PokemonForm form = new PokemonForm();
                form.FormIndex = formNum;
                form.Load(ID);
                Forms.Add(form);
                formNum++;
            }
        }

        
        public void Save() {
            foreach(PokemonForm form in Forms) {
                form.Save(ID);
            }
        }
        */

        public void Save(DatabaseConnection dbConnection)
        {
            var database = dbConnection.Database;

            database.UpdateOrInsert("pokedex_pokemon", new PMDCP.DatabaseConnector.IDataColumn[] {
                database.CreateColumn(true, "DexNum", ID.ToString()),
                database.CreateColumn(false, "PokemonName", Name),
                database.CreateColumn(false, "SpeciesName", SpeciesName),
                database.CreateColumn(false, "GrowthGroup", ((int)GrowthGroup).ToString()),
                database.CreateColumn(false, "EggGroup1", EggGroup1),
                database.CreateColumn(false, "EggGroup2", EggGroup2)
            });

            for (var i = 0; i < Forms.Count; i++)
            {
                var form = Forms[i];

                database.UpdateOrInsert("pokedex_pokemonform", new PMDCP.DatabaseConnector.IDataColumn[] {
                    database.CreateColumn(true, "DexNum", ID.ToString()),
                    database.CreateColumn(true, "FormNum", form.FormIndex.ToString()),
                    database.CreateColumn(false, "FormName", form.FormName),
                    database.CreateColumn(false, "HP", form.BaseHP.ToString()),
                    database.CreateColumn(false, "Attack", form.BaseAtt.ToString()),
                    database.CreateColumn(false, "Defense", form.BaseDef.ToString()),
                    database.CreateColumn(false, "SpecialAttack", form.BaseSpAtt.ToString()),
                    database.CreateColumn(false, "SpecialDefense", form.BaseSpDef.ToString()),
                    database.CreateColumn(false, "Speed", form.BaseSpd.ToString()),
                    database.CreateColumn(false, "Male", form.MaleRatio.ToString()),
                    database.CreateColumn(false, "Female", form.FemaleRatio.ToString()),
                    database.CreateColumn(false, "Height", form.Height.ToString()),
                    database.CreateColumn(false, "Weight", form.Weight.ToString()),
                    database.CreateColumn(false, "Type1", ((int)form.Type1).ToString()),
                    database.CreateColumn(false, "Type2", ((int)form.Type2).ToString()),
                    database.CreateColumn(false, "Ability1", form.Ability1),
                    database.CreateColumn(false, "Ability2", form.Ability2),
                    database.CreateColumn(false, "Ability3", form.Ability3),
                    database.CreateColumn(false, "ExpYield", form.BaseRewardExp.ToString())
                });

                form.SaveMoves(dbConnection, ID, i);
            }
        }

        public void Load(DatabaseConnection dbConnection)
        {
            var database = dbConnection.Database;

            string query = "SELECT pokedex_pokemon.PokemonName, pokedex_pokemon.SpeciesName, " +
                "pokedex_pokemon.GrowthGroup, pokedex_pokemon.EggGroup1, pokedex_pokemon.EggGroup2 " +
                "FROM pokedex_pokemon " +
                "WHERE pokedex_pokemon.DexNum = \'" + ID + "\'";

            DataColumnCollection row = database.RetrieveRow(query);
            if (row != null)
            {
                Name = row["PokemonName"].ValueString;
                SpeciesName = row["SpeciesName"].ValueString;
                GrowthGroup = (Enums.GrowthGroup)row["GrowthGroup"].ValueString.ToInt();
                EggGroup1 = row["EggGroup1"].ValueString;
                EggGroup2 = row["EggGroup2"].ValueString;
            }


            int formNum = 0;
            query = "SELECT pokedex_pokemonform.FormName, pokedex_pokemonform.HP, " +
                "pokedex_pokemonform.Attack, pokedex_pokemonform.Defense, " +
                "pokedex_pokemonform.SpecialAttack, pokedex_pokemonform.SpecialDefense, " +
                "pokedex_pokemonform.Speed, pokedex_pokemonform.Male, pokedex_pokemonform.Female, " +
                "pokedex_pokemonform.Height, pokedex_pokemonform.Weight, " +
                "pokedex_pokemonform.Type1, pokedex_pokemonform.Type2, " +
                "pokedex_pokemonform.Ability1, pokedex_pokemonform.Ability2, pokedex_pokemonform.Ability3, " +
                "pokedex_pokemonform.ExpYield " +
                "FROM pokedex_pokemonform " +
                "WHERE pokedex_pokemonform.DexNum = \'" + ID + "\' " +
                "AND pokedex_pokemonform.FormNum = \'" + formNum + "\'";

            row = database.RetrieveRow(query);
            while (row != null)
            {
                PokemonForm form = new PokemonForm();
                form.FormIndex = formNum;
                form.FormName = row["FormName"].ValueString;
                form.BaseHP = row["HP"].ValueString.ToInt();
                form.BaseAtt = row["Attack"].ValueString.ToInt();
                form.BaseDef = row["Defense"].ValueString.ToInt();
                form.BaseSpAtt = row["SpecialAttack"].ValueString.ToInt();
                form.BaseSpDef = row["SpecialDefense"].ValueString.ToInt();
                form.BaseSpd = row["Speed"].ValueString.ToInt();
                form.MaleRatio = row["Male"].ValueString.ToInt();
                form.FemaleRatio = row["Female"].ValueString.ToInt();
                form.Height = row["Height"].ValueString.ToDbl();
                form.Weight = row["Weight"].ValueString.ToDbl();
                form.Type1 = (Enums.PokemonType)row["Type1"].ValueString.ToInt();
                form.Type2 = (Enums.PokemonType)row["Type2"].ValueString.ToInt();
                form.Ability1 = row["Ability1"].ValueString;
                form.Ability2 = row["Ability2"].ValueString;
                form.Ability3 = row["Ability3"].ValueString;
                form.BaseRewardExp = row["ExpYield"].ValueString.ToInt();

                form.LoadAppearance(dbConnection, ID, formNum);
                form.LoadMoves(dbConnection, ID, formNum);
                Forms.Add(form);

                formNum++;
                query = "SELECT pokedex_pokemonform.FormName, pokedex_pokemonform.HP, " +
                "pokedex_pokemonform.Attack, pokedex_pokemonform.Defense, " +
                "pokedex_pokemonform.SpecialAttack, pokedex_pokemonform.SpecialDefense, " +
                "pokedex_pokemonform.Speed, pokedex_pokemonform.Male, pokedex_pokemonform.Female, " +
                "pokedex_pokemonform.Height, pokedex_pokemonform.Weight, " +
                "pokedex_pokemonform.Type1, pokedex_pokemonform.Type2, " +
                "pokedex_pokemonform.Ability1, pokedex_pokemonform.Ability2, pokedex_pokemonform.Ability3, " +
                "pokedex_pokemonform.ExpYield " +
                "FROM pokedex_pokemonform " +
                "WHERE pokedex_pokemonform.DexNum = \'" + ID + "\' " +
                "AND pokedex_pokemonform.FormNum = \'" + formNum + "\'";
                row = database.RetrieveRow(query);
            }
        }
    }
}
