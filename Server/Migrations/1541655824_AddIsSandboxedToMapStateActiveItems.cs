using System;
using System.Collections.Generic;
using System.Text;
using FluentMigrator;

namespace Server.Migrations
{
    [Tags("data")]
    [Migration(1541655824)]
    public class AddIsSandboxedToMapStateActiveItems : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("is_sandboxed").OnTable("mapstate_activeitem").AsBoolean().WithDefaultValue(false);
        }
    }
}
