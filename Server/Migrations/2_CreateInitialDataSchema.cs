using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Migrations
{
    [Migration(2)]
    public class CreateInitialDataSchema : Migration
    {
        public override void Down()
        {
            Delete.Table("dungeon");
            Delete.Table("dungeon_rmap");
            Delete.Table("dungeon_script");
            Delete.Table("dungeon_smap");
            Delete.Table("evolution");
            Delete.Table("evolution_branch");
            Delete.Table("experience");
            Delete.Table("item");
            Delete.Table("map_data");
            Delete.Table("map_general");
            Delete.Table("map_house_data");
            Delete.Table("map_instanced_data");
            Delete.Table("map_npcs");
            Delete.Table("map_rdungeon_data");
            Delete.Table("map_standard_data");
            Delete.Table("map_switchovers");
            Delete.Table("map_tiles");
            Delete.Table("mapstate_activeitem");
            Delete.Table("mapstate_activenpc_data");
            Delete.Table("mapstate_activenpc_helditem");
            Delete.Table("mapstate_activenpc_location");
            Delete.Table("mapstate_activenpc_moves");
            Delete.Table("mapstate_activenpc_stats");
            Delete.Table("mapstate_activenpc_volatilestatus");
            Delete.Table("mapstate_data");
            Delete.Table("mapstate_mapstatus");
            Delete.Table("mapstate_players_on_map");
            Delete.Table("mission_client");
            Delete.Table("mission_enemy");
            Delete.Table("mission_reward");
            Delete.Table("move");
            Delete.Table("news");
            Delete.Table("npc");
            Delete.Table("npc_drop");
            Delete.Table("packet_statistics");
            Delete.Table("pokedex_pokemon");
            Delete.Table("pokedex_pokemonappearance");
            Delete.Table("pokedex_pokemondwmove");
            Delete.Table("pokedex_pokemoneggmove");
            Delete.Table("pokedex_pokemoneventmove");
            Delete.Table("pokedex_pokemonform");
            Delete.Table("pokedex_pokemonlevelmove");
            Delete.Table("pokedex_pokemontmmove");
            Delete.Table("pokedex_pokemontutormove");
            Delete.Table("rdungeon");
            Delete.Table("rdungeon_chamber");
            Delete.Table("rdungeon_floor");
            Delete.Table("rdungeon_item");
            Delete.Table("rdungeon_npc");
            Delete.Table("rdungeon_tile");
            Delete.Table("rdungeon_weather");
            Delete.Table("shop");
            Delete.Table("shop_trade");
            Delete.Table("start_value");
            Delete.Table("stats_performance");
            Delete.Table("story");
            Delete.Table("story_param");
            Delete.Table("story_segment");
            Delete.Table("title");

            Delete.Schema("pmdcp_data");
        }

        public override void Up()
        {
        }
    }
}
