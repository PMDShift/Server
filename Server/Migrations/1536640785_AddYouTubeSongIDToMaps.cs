using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Migrations
{
    [Tags("data")]
    [Migration(1536640785)]
    public class AddYouTubeSongIDToMaps : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("youtube_music_id").OnTable("map_data").AsString().NotNullable().WithDefaultValue("");
        }
    }
}
