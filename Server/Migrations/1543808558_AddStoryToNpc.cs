using System;
using System.Collections.Generic;
using System.Text;
using FluentMigrator;

namespace Server.Migrations
{
    [Tags("data")]
    [Migration(1543808558)]
    public class AddStoryToNpc : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("story").OnTable("npc").AsInt32().Nullable().WithDefaultValue(0);
        }
    }
}
