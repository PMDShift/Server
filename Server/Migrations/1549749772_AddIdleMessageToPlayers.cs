using System;
using System.Collections.Generic;
using System.Text;
using FluentMigrator;

namespace Server.Migrations
{
    [Tags("players")]
    [Migration(1549749772)]
    public class AddIdleMessageToPlayers : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("IdleMessage").OnTable("characteristics").AsString();
        }
    }
}
