using System;
using System.Collections.Generic;
using System.Text;
using FluentMigrator;

namespace Server.Migrations
{
    [Tags("data")]
    [Migration(1539925336)]
    public class AddYouTubeVideoIdToRDungeonFloors : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("youtube_video_id").OnTable("rdungeon_floor").AsString().Nullable();
        }
    }
}
