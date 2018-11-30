using System;
using System.Collections.Generic;
using System.Text;
using FluentMigrator;

namespace Server.Migrations
{
    [Tags("data")]
    [Migration(1543558314)]
    class AddSpawnDirectionToNPCs : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("spawn_direction").OnTable("npc").AsInt32().Nullable().WithDefaultValue(0);
        }
    }
}
