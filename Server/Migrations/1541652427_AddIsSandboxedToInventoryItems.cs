using System;
using System.Collections.Generic;
using System.Text;
using FluentMigrator;

namespace Server.Migrations
{
    [Tags("players")]
    [Migration(1541652427)]
    public class AddIsSandboxedToInventoryItems : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("is_sandboxed").OnTable("inventory").AsBoolean().NotNullable().WithDefaultValue(false);
        }
    }
}
