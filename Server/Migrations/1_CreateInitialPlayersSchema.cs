using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Migrations
{
    [Migration(1)]
    public class CreateInitialPlayersSchema: Migration
    {
        public override void Down()
        {
            Delete.Table("accounts");
            Delete.Table("bank");
            Delete.Table("bans");
            Delete.Table("character_statistics");
            Delete.Table("characteristics");
            Delete.Table("characters");
            Delete.Table("dungeons");
            Delete.Table("expkit");
            Delete.Table("friends");
            Delete.Table("guild");
            Delete.Table("inventory");
            Delete.Table("items");
            Delete.Table("job_list");
            Delete.Table("location");
            Delete.Table("map_load_trigger_event");
            Delete.Table("mission_board_missions");
            Delete.Table("missions");
            Delete.Table("parties");
            Delete.Table("recruit_data");
            Delete.Table("recruit_list");
            Delete.Table("recruit_moves");
            Delete.Table("recruit_volatile_status");
            Delete.Table("script_extras_general");
            Delete.Table("step_counter_trigger_event");
            Delete.Table("stepped_on_tile_trigger_event");
            Delete.Table("story");
            Delete.Table("story_chapters");
            Delete.Table("story_helper_state_settings");
            Delete.Table("team");
            Delete.Table("trigger_events");
        }

        public override void Up()
        {
        }
    }
}
