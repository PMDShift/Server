using System;
using System.Collections.Generic;
using System.Text;
using FluentMigrator;

namespace Server.Migrations
{
    [Tags("data")]
    [Migration(1543560965)]
    public class AddSpawnWeatherToNPCs : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("spawn_weather").OnTable("npc").AsInt32().Nullable().WithDefaultValue(0);
        }
    }
}
