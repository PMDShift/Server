using System;
using System.Collections.Generic;
using System.Text;
using FluentMigrator;

namespace Server.Migrations
{
    [Tags("data")]
    [Migration(1544073464)]
    public class AddEffectToMaps : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("effect").OnTable("map_data").AsInt32().NotNullable().WithDefaultValue(0);
        }
    }
}
