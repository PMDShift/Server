using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jirapi;

namespace Server.Pokedex
{
    public class PokedexSync
    {
        HashSet<string> missingMoves = new HashSet<string>();

        public async Task SyncPokedex(int startingDexNum, int endingDexNum)
        {
            using (var dbConnection = new Database.DatabaseConnection(Database.DatabaseID.Data))
            {

                var client = new PokeClient();

                for (var i = startingDexNum; i <= endingDexNum; i++)
                {
                    var pokemon = await client.Get<Jirapi.Resources.Pokemon>(i);

                    var species = await client.Get<Jirapi.Resources.PokemonSpecies>(i);

                    var pokedexEntry = Pokedex.GetPokemon(i);

                    pokedexEntry.Name = species.Names.Where(x => x.Language.Name == "en").First().Name1;
                    pokedexEntry.SpeciesName = species.Genera.Where(x => x.Language.Name == "en").First().Genus1;

                    var growthGroup = species.GrowthRate.Name;
                    switch (growthGroup.ToLower())
                    {
                        case "slow":
                            pokedexEntry.GrowthGroup = Enums.GrowthGroup.Slow;
                            break;
                        case "medium":
                            pokedexEntry.GrowthGroup = Enums.GrowthGroup.Fluctuating;
                            break;
                        case "medium-slow":
                            pokedexEntry.GrowthGroup = Enums.GrowthGroup.MediumSlow;
                            break;
                        case "medium-fast":
                            pokedexEntry.GrowthGroup = Enums.GrowthGroup.MediumFast;
                            break;
                        case "fast":
                            pokedexEntry.GrowthGroup = Enums.GrowthGroup.Fast;
                            break;
                        case "erratic":
                            pokedexEntry.GrowthGroup = Enums.GrowthGroup.Erratic;
                            break;
                        default:
                            throw new NotImplementedException();
                    }

                    foreach (var eggGroup in species.EggGroups)
                    {
                        await eggGroup.FillResource();
                    }

                    if (species.EggGroups.Count > 0)
                    {
                        pokedexEntry.EggGroup1 = species.EggGroups[0].Resource.Names.Where(x => x.Language.Name == "en").First().Name1;
                    }

                    if (species.EggGroups.Count > 1)
                    {
                        pokedexEntry.EggGroup2 = species.EggGroups[1].Resource.Names.Where(x => x.Language.Name == "en").First().Name1;
                    }

                    // Load abilities
                    foreach (var ability in pokemon.Abilities)
                    {
                        await ability.Ability.FillResource();
                    }

                    for (var n = 0; n < pokemon.Forms.Count; n++)
                    {
                        var form = pokemon.Forms[n];

                        await form.FillResource();

                        PokemonForm pokedexForm;
                        if (pokedexEntry.Forms.Count > n)
                        {
                            pokedexForm = pokedexEntry.Forms[n];
                        }
                        else
                        {
                            pokedexForm = new PokemonForm();

                            pokedexEntry.Forms.Add(pokedexForm);
                        }

                        if (string.IsNullOrEmpty(form.Resource.FormName))
                        {
                            pokedexForm.FormName = "Normal";
                        }
                        else
                        {
                            pokedexForm.FormName = form.Resource.FormName;
                        }

                        pokedexForm.Weight = pokemon.Weight;
                        pokedexForm.Height = pokemon.Height;
                        pokedexForm.BaseRewardExp = pokemon.BaseExperience;

                        pokedexForm.BaseHP = pokemon.Stats.Where(x => x.Stat.Name == "hp").First().BaseStat;
                        pokedexForm.BaseAtt = pokemon.Stats.Where(x => x.Stat.Name == "attack").First().BaseStat;
                        pokedexForm.BaseDef = pokemon.Stats.Where(x => x.Stat.Name == "defense").First().BaseStat;
                        pokedexForm.BaseSpAtt = pokemon.Stats.Where(x => x.Stat.Name == "special-attack").First().BaseStat;
                        pokedexForm.BaseSpDef = pokemon.Stats.Where(x => x.Stat.Name == "special-defense").First().BaseStat;
                        pokedexForm.BaseSpd = pokemon.Stats.Where(x => x.Stat.Name == "speed").First().BaseStat;

                        if (pokemon.Types.Count > 0)
                        {
                            pokedexForm.Type1 = MapTypeNameToType(pokemon.Types[0].Type.Name);
                        }
                        if (pokemon.Types.Count > 1)
                        {
                            pokedexForm.Type2 = MapTypeNameToType(pokemon.Types[1].Type.Name);
                        }

                        pokedexForm.Ability1 = "None";
                        pokedexForm.Ability2 = "None";
                        pokedexForm.Ability3 = "None";
                        if (pokemon.Abilities.Count > 0)
                        {
                            pokedexForm.Ability1 = pokemon.Abilities[0].Ability.Resource.Names.Where(x => x.Language.Name == "en").First().Name1;
                        }
                        if (pokemon.Abilities.Count > 1)
                        {
                            pokedexForm.Ability2 = pokemon.Abilities[1].Ability.Resource.Names.Where(x => x.Language.Name == "en").First().Name1;
                        }
                        if (pokemon.Abilities.Count > 2)
                        {
                            pokedexForm.Ability3 = pokemon.Abilities[2].Ability.Resource.Names.Where(x => x.Language.Name == "en").First().Name1;
                        }

                        pokedexForm.LevelUpMoves.Clear();
                        pokedexForm.TMMoves.Clear();
                        pokedexForm.TutorMoves.Clear();
                        pokedexForm.EggMoves.Clear();
                        foreach (var move in pokemon.Moves)
                        {
                            var versionGroupDetails = move.VersionGroupDetails.First();

                            switch (versionGroupDetails.MoveLearnMethod.Name.ToLower())
                            {
                                case "level-up":
                                    {
                                        var moveId = FindMoveByName(move.Move.Name);

                                        if (moveId > -1)
                                        {
                                            pokedexForm.LevelUpMoves.Add(new LevelUpMove(moveId, versionGroupDetails.LevelLearnedAt));
                                        }
                                    }
                                    break;
                                case "machine":
                                    {
                                        var moveId = FindMoveByName(move.Move.Name);

                                        if (moveId > -1)
                                        {
                                            pokedexForm.TMMoves.Add(moveId);
                                        }
                                    }
                                    break;
                                case "tutor":
                                    {
                                        var moveId = FindMoveByName(move.Move.Name);

                                        if (moveId > -1)
                                        {
                                            pokedexForm.TutorMoves.Add(moveId);
                                        }
                                    }
                                    break;
                                case "egg":
                                    {
                                        var moveId = FindMoveByName(move.Move.Name);

                                        if (moveId > -1)
                                        {
                                            pokedexForm.EggMoves.Add(moveId);
                                        }
                                    }
                                    break;
                                default:
                                    throw new NotImplementedException();
                            }
                        }
                    }

                    Pokedex.SavePokemon(dbConnection, i);
                }
            }
        }

        private int FindMoveByName(string name)
        {
            for (var i = 1; i < Moves.MoveManager.Moves.MaxMoves; i++)
            {
                var move = Moves.MoveManager.Moves[i];

                if (move.Name.ToLower().Replace(' ', '-').Trim('`').Trim('\'') == name)
                {
                    return i;
                }
            }

            if (!missingMoves.Contains(name))
            {
                Console.WriteLine($"MISSING MOVE: {name}");
                missingMoves.Add(name);
            }
            return -1;
        }

        private Enums.PokemonType MapTypeNameToType(string typeName)
        {
            switch (typeName.ToLower())
            {
                case "bug":
                    return Enums.PokemonType.Bug;
                case "dark":
                    return Enums.PokemonType.Dark;
                case "dragon":
                    return Enums.PokemonType.Dragon;
                case "electric":
                    return Enums.PokemonType.Electric;
                case "fairy":
                    return Enums.PokemonType.Fairy;
                case "fighting":
                    return Enums.PokemonType.Fighting;
                case "fire":
                    return Enums.PokemonType.Fire;
                case "flying":
                    return Enums.PokemonType.Flying;
                case "ghost":
                    return Enums.PokemonType.Ghost;
                case "grass":
                    return Enums.PokemonType.Grass;
                case "ground":
                    return Enums.PokemonType.Ground;
                case "ice":
                    return Enums.PokemonType.Ice;
                case "normal":
                    return Enums.PokemonType.Normal;
                case "poison":
                    return Enums.PokemonType.Poison;
                case "psychic":
                    return Enums.PokemonType.Psychic;
                case "rock":
                    return Enums.PokemonType.Rock;
                case "steel":
                    return Enums.PokemonType.Steel;
                case "water":
                    return Enums.PokemonType.Water;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
