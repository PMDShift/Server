using System;
using System.Collections.Generic;
using System.Text;
using FluentMigrator;

namespace Server.Migrations
{
    [Tags("data")]
    [Migration(1544866465)]
    public class AddUnrecruitableToMapNpcs : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("Unrecruitable").OnTable("mapstate_activenpc_data").AsBoolean().WithDefaultValue(false);
        }
    }
}
