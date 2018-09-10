using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Migrations
{
    [Tags("players")]
    [Migration(1536556104)]
    public class SetupInitialSandboxColumns : AutoReversingMigration
    {
        public override void Up() {
            Create.Column("IsSandboxed").OnTable("characteristics").AsBoolean().NotNullable().WithDefaultValue(false);
        }
    }
}
