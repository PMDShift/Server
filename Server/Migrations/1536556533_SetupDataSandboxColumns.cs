using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Migrations
{
    [Tags("data")]
    [Migration(1536556533)]
    public class SetupDataSandboxColumns : AutoReversingMigration
    {
        public override void Up() {
            Create.Column("IsSandboxed").OnTable("map_data").AsBoolean().NotNullable().WithDefaultValue(true);
            Create.Column("ZoneID").OnTable("map_data").AsInt32().Nullable();

            Create.Column("is_sandboxed").OnTable("rdungeon").AsBoolean().NotNullable().WithDefaultValue(true);
            Create.Column("zone_id").OnTable("rdungeon").AsInt32().Nullable();

            Create.Column("is_sandboxed").OnTable("dungeon").AsBoolean().NotNullable().WithDefaultValue(true);
            Create.Column("zone_id").OnTable("dungeon").AsInt32().Nullable();

            Create.Column("is_sandboxed").OnTable("item").AsBoolean().NotNullable().WithDefaultValue(true);
            Create.Column("zone_id").OnTable("item").AsInt32().Nullable();

            Create.Column("is_sandboxed").OnTable("npc").AsBoolean().NotNullable().WithDefaultValue(true);
            Create.Column("zone_id").OnTable("npc").AsInt32().Nullable();

            Create.Column("is_sandboxed").OnTable("shop").AsBoolean().NotNullable().WithDefaultValue(true);
            Create.Column("zone_id").OnTable("shop").AsInt32().Nullable();

            Create.Column("is_sandboxed").OnTable("story").AsBoolean().NotNullable().WithDefaultValue(true);
            Create.Column("zone_id").OnTable("story").AsInt32().Nullable();

            Create.Column("is_sandboxed").OnTable("move").AsBoolean().NotNullable().WithDefaultValue(true);
            Create.Column("is_sandboxed").OnTable("evolution").AsBoolean().NotNullable().WithDefaultValue(true);
        }
    }
}
