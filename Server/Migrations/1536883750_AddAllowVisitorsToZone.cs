using System;
using System.Collections.Generic;
using System.Text;
using FluentMigrator;

namespace Server.Migrations
{
    [Tags("data")]
    [Migration(1536883750)]
    public class AddAllowVisitorsToZone : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("allow_visitors").OnTable("zone").AsBoolean();
        }
    }
}
