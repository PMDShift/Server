using System;
using System.Collections.Generic;
using System.Text;
using FluentMigrator;

namespace Server.Migrations
{
    [Tags("data")]
    [Migration(1536800576)]
    public class SetupZoneTables : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("zone")
                  .WithColumn("num").AsInt32().PrimaryKey().NotNullable()
                  .WithColumn("name").AsString().NotNullable()
                  .WithColumn("is_open").AsBoolean().NotNullable().WithDefaultValue(false)
                  .WithColumn("discord_channel_id").AsInt64().NotNullable().WithDefaultValue(0);

            Create.Table("zone_member")
                  .WithColumn("zone_id").AsInt32().PrimaryKey().NotNullable()
                  .WithColumn("character_id").AsString().PrimaryKey().NotNullable()
                  .WithColumn("access").AsInt32().NotNullable().WithDefaultValue(0);
        }
    }
}
