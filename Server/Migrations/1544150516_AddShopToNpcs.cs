using System;
using System.Collections.Generic;
using System.Text;
using FluentMigrator;

namespace Server.Migrations
{
    [Tags("data")]
    [Migration(1544150516)]
    public class AddShopToNpcs : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("shop").OnTable("npc").AsInt32().Nullable().WithDefaultValue(0);
        }
    }
}
