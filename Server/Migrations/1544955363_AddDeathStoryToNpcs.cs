using System;
using System.Collections.Generic;
using System.Text;
using FluentMigrator;

namespace Server.Migrations
{
    [Tags("data")]
    [Migration(1544955363)]
    public class AddDeathStoryToNpcs : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("death_story").OnTable("npc").AsInt32().Nullable().WithDefaultValue(-1);
        }
    }
}
